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
    public class CaseService : ICaseService
    {
        private readonly PetShopContext _serviceContext;

        public CaseService(PetShopContext serviceContext)
        {
            _serviceContext = serviceContext;
        }

        public async Task CreateCaseAsync(Case _case)
        {
            using var transaction = await _serviceContext.Database.BeginTransactionAsync();
            try
            {
                var newCase = new Case
                {
                    CaseId = _case.CaseId,
                    FacilityId = _case.FacilityId,
                    PetId = _case.PetId,
                    StartTime = _case.StartTime,
                    EndTime = _case.EndTime,
                    Notes = _case.Notes,
                    IsClosed = _case.IsClosed,
                    IsDeleted = false
                };

                await _serviceContext.Cases.AddAsync(newCase);
                await _serviceContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Case>> GetCasesAsync()
        {
            return await _serviceContext.Cases
                .Where(c => c.IsDeleted != true)
                .Include(c => c.Facility)
                .Include(c => c.Pet)
                .Include(c => c.Invoices)
                .Include(c => c._PlannedServices)
                .ToListAsync();
        }

        public async Task<Case?> GetCaseByIdAsync(Guid id)
        {
            return await _serviceContext.Cases
                .Include(c => c.Facility)
                .Include(c => c.Pet)
                .Include(c => c.Invoices)
                .Include(c => c._PlannedServices)
                .FirstOrDefaultAsync(c => c.CaseId == id && c.IsDeleted != true);
        }

        public async Task<bool> UpdateCategoryAsync(Case _case, Guid id)
        {
            var caseToUpdate = await _serviceContext.Cases.FirstOrDefaultAsync(c => c.CaseId == id);

            if (caseToUpdate == null)
            {
                return false;
            }

            caseToUpdate.FacilityId = _case.FacilityId;
            caseToUpdate.PetId = _case.PetId;
            caseToUpdate.StartTime = _case.StartTime;
            caseToUpdate.EndTime = _case.EndTime;
            caseToUpdate.Notes = _case.Notes;
            caseToUpdate.IsClosed = _case.IsClosed;

            _serviceContext.Cases.Update(caseToUpdate);
            return await _serviceContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCaseAsync(Guid id)
        {
            var caseToDelete = await _serviceContext.Cases
                .Include(c => c._PlannedServices)
                .FirstOrDefaultAsync(c => c.CaseId == id);

            if (caseToDelete == null) return false;

            if (caseToDelete._PlannedServices != null && caseToDelete._PlannedServices.Any())
            {
                caseToDelete.IsDeleted = true;
            }
            else
            {
                _serviceContext.Cases.Remove(caseToDelete);
            }

            return await _serviceContext.SaveChangesAsync() > 0;
        }
    }
}
