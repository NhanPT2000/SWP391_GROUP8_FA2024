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
    public class SpeciesService : ISpeciesService
    {
        private readonly PetShopContext _context;
        public SpeciesService(PetShopContext context)
        {
            _context = context;
        }
        public async Task CreateSpecie(Species species)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newSpecie = new Species
                {
                    SpeciesId = species.SpeciesId,
                    SpeciesName = species.SpeciesName
                };

                await _context._Species.AddAsync(newSpecie);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool?> DeleteSpecie(Guid id)
        {
            var specieToDelete = await _context._Species.Include(s => s.Pets).FirstOrDefaultAsync(s => s.SpeciesId == id);
            if (specieToDelete == null) return false;
            if(specieToDelete.Pets != null && specieToDelete.Pets.Any()) specieToDelete.IsDelete = true;
            else _context._Species.Remove(specieToDelete);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Species>> GetAll()
        {
            return await _context._Species.Where(s => s.IsDelete != true).ToListAsync();
        }

        public async Task<Species?> GetSpeciesById(Guid id)
        {
            var specie = await _context._Species.FindAsync(id);
            if (specie == null) return default;
            return specie;
        }

        public async Task<bool?> UpdateSpecie(Guid id, Species species)
        {
            var specieToUpdate = await _context._Species.FirstOrDefaultAsync(p => p.SpeciesId == id);

            if (specieToUpdate == null)
            {
                return false;
            }
            specieToUpdate.SpeciesName = species.SpeciesName;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
