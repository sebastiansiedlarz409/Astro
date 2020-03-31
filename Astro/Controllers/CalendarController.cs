using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Astro.DAL.DBContext;
using Astro.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Astro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly AstroDbContext _context;

        public CalendarController(AstroDbContext context)
        {
            _context = context;
        }

        //GET api/Calendar
        [HttpGet]
        public async Task<IEnumerable<CalendarItem>> Get()
        {
            List<CalendarEvent> calendarEvents = await _context.CalendarEvents.AsNoTracking().ToListAsync();

            return calendarEvents.ToList().Select(t => (CalendarItem)t);
        }

        //GET api/Calendar/1
        [HttpGet("{id}")]
        public async Task<CalendarItem> Get(int id)
        {
            return (CalendarItem)await _context.CalendarEvents.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        //POST api/Calendar
        [HttpPost]
        public async Task<ObjectResult> Post([FromForm]CalendarItem item)
        {
            CalendarEvent calendarEvent = (CalendarEvent)item;

            await _context.CalendarEvents.AddAsync(calendarEvent);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                tid = calendarEvent.Id,
                action = "inserted"
            });
        }

        //PUT api/Calendar/1
        [HttpPut("{id}")]
        public async Task<ObjectResult> Put(int id, [FromForm] CalendarItem item)
        {
            CalendarEvent calendarEvent = (CalendarEvent)item;

            CalendarEvent oldCalendarEvent = await _context.CalendarEvents.FirstOrDefaultAsync(t => t.Id == id);

            oldCalendarEvent.Name = calendarEvent.Name;
            oldCalendarEvent.StartDate = calendarEvent.StartDate;
            oldCalendarEvent.EndDate = calendarEvent.EndDate;

            _context.CalendarEvents.Update(oldCalendarEvent);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                action = "updated"
            });
        }

        //DELETE api/Calendar/1
        [HttpDelete("{id}")]
        public async Task<ObjectResult> Delete(int id)
        {
            CalendarEvent calendarEvent = await _context.CalendarEvents.FirstOrDefaultAsync(t => t.Id == id);

            if (calendarEvent != null)
            {
                _context.CalendarEvents.Remove(calendarEvent);
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }
    }
}