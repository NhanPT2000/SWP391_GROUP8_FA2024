using DataAccess.Database;
using DataAccess.Repository;
using DataObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class PetService : IPetService
    {
        private readonly PetShopContext _serviceContext;
        public PetService(PetShopContext serviceContext)
        {
            _serviceContext = serviceContext;
        }
        public async Task CreatePetAsync(Pet pet)
        {
            using var transaction = await _serviceContext.Database.BeginTransactionAsync();
            try
            {
                var newPet = new Pet
                {
                    Birthdate = DateTime.Now,
                    Image = pet.Image,
                    MemberId = pet.MemberId,
                    Notes = pet.Notes,
                    PetId = pet.PetId,
                    SpeciesId = pet.SpeciesId,
                    PetName = pet.PetName
                };

                await _serviceContext.Pets.AddAsync(newPet);
                await _serviceContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeletePetAsync(Guid id)
        {
            var petToDelete = await _serviceContext.Pets.Include(p => p._Cases).FirstOrDefaultAsync(p => p.PetId == id);
            if (petToDelete == null) return false;

            if(petToDelete._Cases != null && petToDelete._Cases.Any()) petToDelete.IsDeleted = true;
            else _serviceContext.Pets.Remove(petToDelete);
            return await _serviceContext.SaveChangesAsync() > 0;
        }

        public async Task<Pet?> GetPetByIdAsync(Guid id)
        {
            var pet = await _serviceContext.Pets
                .Include(p => p.Species)
                .FirstOrDefaultAsync(p => p.PetId == id);
            if(pet == null) return default;
            return pet;
        }

        public async Task<IEnumerable<Pet>> GetPetsAsync(Guid id)
        {
            return await (from pet in _serviceContext.Pets where pet.MemberId == id select pet)
                .Include(p => p.Member)
                .Include(p => p.Species)
                .Where(p => p.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<bool> UpdatePetAsync(Pet pet, Guid id)
        {
            var petToUpdate = await _serviceContext.Pets.FirstOrDefaultAsync(p => p.PetId == id);

            if (petToUpdate == null)
            {
                return false;
            }
            petToUpdate.Birthdate = pet.Birthdate;
            petToUpdate.PetName = pet.PetName;
            petToUpdate.Notes = pet.Notes;
            petToUpdate.SpeciesId = pet.SpeciesId;
            petToUpdate.Image = pet.Image;
            _serviceContext.Pets.Update(petToUpdate);
            return await _serviceContext.SaveChangesAsync() > 0;
        }
    }
}
