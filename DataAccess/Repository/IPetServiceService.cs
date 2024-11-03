using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IPetServiceService
    {
        Task<IEnumerable<DataObject.Service>> GetPetServicesAsync();
        Task<DataObject.Service?> GetServiceByIdAsync(Guid id);
        Task<bool> UpdateServiceAsync(DataObject.Service service, Guid id);
        Task<bool> DeleteServiceAsync(Guid id);
        Task CreatePetServiceAsync(DataObject.Service service);
    }
}
