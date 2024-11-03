using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IPlannedServiceService
    {
        Task<IEnumerable<PlannedService>> GetPlannedServicesAsync();
        Task<Category?> GetPlannedServiceByIdAsync(Guid id);
        Task<bool> UpdatePlannedServiceAsync(PlannedService plannedService, Guid id);
        Task<bool> DeletePlannedServiceAsync(Guid id);
        Task CreatePlannedServiceAsync(PlannedService plannedService);
    }
}
