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
    public class FacilityService : IFacilityService
    {
        private readonly PetShopContext _serviceContext;

        public FacilityService(PetShopContext serviceContext)
        {
            _serviceContext = serviceContext;
        }

        public async Task CreateFacilityAsync(Facility facility)
        {
            using var transaction = await _serviceContext.Database.BeginTransactionAsync();
            try
            {
                var newFacility = new Facility
                {
                    FacilityId = facility.FacilityId,
                    FacilityName = facility.FacilityName,
                    Address = facility.Address,
                    PhoneNumber = facility.PhoneNumber,
                    ContractPer = facility.ContractPer,
                    Image = facility.Image,
                    IsDeleted = false
                };

                await _serviceContext.Facilities.AddAsync(newFacility);
                await _serviceContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Facility>> GetFacilitiesAsync()
        {
            return await _serviceContext.Facilities
                .Where(f => f.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<Facility?> GetFacilityByIdAsync(Guid id)
        {
            return await _serviceContext.Facilities
                .FirstOrDefaultAsync(f => f.FacilityId == id && f.IsDeleted != true);
        }

        public async Task<bool> UpdateFacilityAsync(Facility facility, Guid id)
        {
            var facilityToUpdate = await _serviceContext.Facilities
                .FirstOrDefaultAsync(f => f.FacilityId == id);

            if (facilityToUpdate == null)
            {
                return false;
            }

            facilityToUpdate.FacilityName = facility.FacilityName;
            facilityToUpdate.Address = facility.Address;
            facilityToUpdate.PhoneNumber = facility.PhoneNumber;
            facilityToUpdate.ContractPer = facility.ContractPer;
            facilityToUpdate.Image = facility.Image;

            _serviceContext.Facilities.Update(facilityToUpdate);
            return await _serviceContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFacilityAsync(Guid id)
        {
            var facilityToDelete = await _serviceContext.Facilities
                .Include(f => f._Cases)
                .FirstOrDefaultAsync(f => f.FacilityId == id);
            if (facilityToDelete == null) return false;

            if (facilityToDelete._Cases != null && facilityToDelete._Cases.Any())
            {
                facilityToDelete.IsDeleted = true;
            }
            else
            {
                _serviceContext.Facilities.Remove(facilityToDelete);
            }

            return await _serviceContext.SaveChangesAsync() > 0;
        }
    }
}
