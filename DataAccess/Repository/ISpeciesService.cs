using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISpeciesService
    {
        Task CreateSpecie(Species species);
        Task<bool?> DeleteSpecie(Guid id);
        Task<bool?> UpdateSpecie(Guid id, Species species);
        Task<Species?> GetSpeciesById(Guid id);
        Task<IEnumerable<Species>> GetAll();
    }
}
