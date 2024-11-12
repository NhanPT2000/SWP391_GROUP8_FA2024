using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync(Guid id);
        Task<Event?> GetEventByIdAsync(Guid id);
        Task<bool> UpdateEventAsync(Event _event, Guid id);
        Task<bool> DeleteEventAsync(Guid id);
        Task CreateEventAsync(Event _event);
        Task<IEnumerable<Event>> GetAllEventsAsync();
    }
}
