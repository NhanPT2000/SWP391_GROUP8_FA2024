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
    public class PlannedServiceService : IPlannedServiceService
    {
        private readonly PetShopContext _serviceContext;

        public PlannedServiceService(PetShopContext serviceContext)
        {
            _serviceContext = serviceContext;
        }

        public async Task CreatePlannedServiceAsync(PlannedService plannedService)
        {
            using var transaction = await _serviceContext.Database.BeginTransactionAsync();
            try
            {
                var newPlannedService = new PlannedService
                {
                    PlannedServiceId = plannedService.PlannedServiceId,
                    CaseId = plannedService.CaseId,
                    ServiceId = plannedService.ServiceId,
                    StartTime = plannedService.StartTime,
                    EndTime = plannedService.EndTime,
                    PlannedUnits = plannedService.PlannedUnits,
                    CostPerUnit = plannedService.CostPerUnit,
                    Price = plannedService.Price,
                    Notes = plannedService.Notes,
                    MemberId = plannedService.MemberId,
                    IsDeleted = false,
                    IsCofirmed = false
                };

                await _serviceContext.PlannedServices.AddAsync(newPlannedService);
                await _serviceContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<PlannedService>> GetPlannedServicesAsync()
        {
            return await _serviceContext.PlannedServices
                .Where(ps => ps.IsDeleted != true)
                .Include(ps => ps.Case)
                .ThenInclude(c => c.Pet)
                .Include(ps => ps.Case)
                .ThenInclude(c => c.Facility)
                .Include(ps => ps.Service)
                .Include(ps => ps._Vouchers)
                .Include(ps => ps._Member)
                .Where(ps => ps.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<PlannedService?> GetPlannedServiceByIdAsync(Guid id)
        {
            return await _serviceContext.PlannedServices
                .Include(ps => ps.Case)
                .ThenInclude(c => c.Pet)
                .Include(ps => ps.Case)
                .ThenInclude(c => c.Facility)
                .Include(ps => ps.Service)
                .Include(ps => ps._Vouchers)
                .Include(ps => ps._Member)
                .FirstOrDefaultAsync(ps => ps.PlannedServiceId == id && ps.IsDeleted != true);
        }

        public async Task<IEnumerable<PlannedService>?> GetPlannedServicesByIdAsync(Guid id)
        {
            return await _serviceContext.PlannedServices
                .Include(ps => ps.Case)
                .ThenInclude(c => c.Pet)
                .Include(ps => ps.Case)
                .ThenInclude(c => c.Facility)
                .Include(ps => ps.Service)
                .Include(ps => ps._Vouchers)
                .Include(ps => ps._Member)
                .Where(ps => ps.MemberId == id && ps.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<bool> UpdatePlannedServiceAsync(PlannedService plannedService, Guid id)
        {
            var plannedServiceToUpdate = await _serviceContext.PlannedServices.FirstOrDefaultAsync(ps => ps.PlannedServiceId == id);

            if (plannedServiceToUpdate == null)
            {
                return false;
            }

            plannedServiceToUpdate.CaseId = plannedService.CaseId;
            plannedServiceToUpdate.ServiceId = plannedService.ServiceId;
            plannedServiceToUpdate.StartTime = plannedService.StartTime;
            plannedServiceToUpdate.EndTime = plannedService.EndTime;
            plannedServiceToUpdate.PlannedUnits = plannedService.PlannedUnits;
            plannedServiceToUpdate.CostPerUnit = plannedService.CostPerUnit;
            plannedServiceToUpdate.Price = plannedService.Price;
            plannedServiceToUpdate.Notes = plannedService.Notes;
            plannedServiceToUpdate.MemberId = plannedService.MemberId;
            plannedService.IsDeleted = plannedServiceToUpdate.IsDeleted;
            plannedServiceToUpdate.IsCofirmed = plannedService.IsCofirmed;

            _serviceContext.PlannedServices.Update(plannedServiceToUpdate);
            return await _serviceContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePlannedServiceAsync(Guid id)
        {
            var plannedServiceToDelete = await _serviceContext.PlannedServices
                .Include(ps => ps._Member)
                .FirstOrDefaultAsync(ps => ps.PlannedServiceId == id);

            if (plannedServiceToDelete == null) return false;

            if (plannedServiceToDelete._Member!= null)
            {
                plannedServiceToDelete.IsDeleted = true;
            }
            else
            {
                _serviceContext.PlannedServices.Remove(plannedServiceToDelete);
            }

            return await _serviceContext.SaveChangesAsync() > 0;
        }
    }
}
