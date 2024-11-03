using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IFacilityService
    {
        Task<IEnumerable<Facility>> GetFacilitiesAsync();
        Task<Facility?> GetFacilityByIdAsync(Guid id);
        Task<bool> UpdateFacilityAsync(Facility facility, Guid id);
        Task<bool> DeleteFacilityAsync(Guid id);
        Task CreateFacilityAsync(Facility facility);
    }
}
