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
    public class PetServiceService : IPetServiceService
    {
        private readonly PetShopContext _dbContext;
        public PetServiceService(PetShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreatePetServiceAsync(DataObject.Service service)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var newService = new DataObject.Service
                {
                    ServiceId = service.ServiceId,
                    CostPerUnity = service.CostPerUnity,
                    HasLimit = service.HasLimit,
                    ServiceDescription = service.ServiceDescription,
                    ServiceName = service.ServiceName,
                    Image = service.Image
                };

                await _dbContext.Services.AddAsync(newService);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteServiceAsync(Guid id)
        {
            var serviceToDelete = await _dbContext.Services.Include(s => s._PlannedServices).FirstOrDefaultAsync(p => p.ServiceId == id);

            if (serviceToDelete == null)
            {
                return false;
            }

            if (serviceToDelete._PlannedServices != null && serviceToDelete._PlannedServices.Any()) serviceToDelete.IsDeleted = true;
            else _dbContext.Remove(serviceToDelete);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<DataObject.Service>> GetPetServicesAsync()
        {
            return await _dbContext.Services.Where(s => s.IsDeleted !=true).ToListAsync();
        }

        public async Task<DataObject.Service?> GetServiceByIdAsync(Guid id)
        {
            var service = await _dbContext.Services.FindAsync(id);

            if (service == null)
            {
                return default;
            }
            return service;
        }

        public async Task<bool> UpdateServiceAsync(DataObject.Service service, Guid id)
        {
            var serviceToUpdate = await _dbContext.Services.FindAsync(id);

            if (serviceToUpdate == null)
            {
                return false;
            }
            serviceToUpdate.ServiceDescription = service.ServiceDescription;
            serviceToUpdate.ServiceName = service.ServiceName;
            serviceToUpdate.CostPerUnity = service.CostPerUnity;
            serviceToUpdate.HasLimit = service.HasLimit;
            serviceToUpdate.Image = service.Image;
            _dbContext.Services.Update(serviceToUpdate);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
