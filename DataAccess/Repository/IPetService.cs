using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> GetPetsAsync(Guid id);
        Task<Pet?> GetPetByIdAsync(Guid id);
        Task<bool> UpdatePetAsync(Pet pet, Guid id);
        Task<bool> DeletePetAsync(Guid id);
        Task CreatePetAsync(Pet pet);
    }
}
