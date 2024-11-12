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
    public class EventService : IEventService
    {
        private readonly PetShopContext _context;
        public EventService(PetShopContext context) 
        {
            _context = context;
        }
        public async Task CreateEventAsync(Event _event)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newEvent = new Event
                {
                    AdminId = _event.AdminId,
                    Description = _event.Description,
                    EventId = _event.EventId,
                    EventTitle = _event.EventTitle,
                    Image = _event.Image,
                    PostDate = DateTime.UtcNow
                };

                await _context.Events.AddAsync(newEvent);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteEventAsync(Guid id)
        {
            var eventToDelete = await _context.Events.FirstAsync(p => p.EventId == id);
            if (eventToDelete == null) return false;
            _context.Events.Remove(eventToDelete);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Event?> GetEventByIdAsync(Guid id)
        {
            var _event = await _context.Events
                .Include(p => p.Admin)
                .FirstOrDefaultAsync(p => p.EventId == id);
            if (_event == null) return default;
            return _event;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(Guid id)
        {
            return await(from _event in _context.Events where _event.AdminId == id select _event)
                .Include(p => p.Admin)
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await (from _event in _context.Events select _event)
                .Include(p => p.Admin)
                .ToListAsync();
        }

        public async Task<bool> UpdateEventAsync(Event _event, Guid id)
        {
            var eventToUpdate = await _context.Events.FirstOrDefaultAsync(p => p.EventId == id);

            if (eventToUpdate == null)
            {
                return false;
            }
            eventToUpdate.PostDate = DateTime.UtcNow;
            eventToUpdate.Image = _event.Image;
            eventToUpdate.EventTitle = _event.EventTitle;
            eventToUpdate.Description = _event.Description;
            _context.Events.Update(eventToUpdate);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
