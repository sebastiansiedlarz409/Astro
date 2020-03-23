using System.Collections.Generic;
using System.Linq;
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
        private AstroDbContext _context;

        public CalendarController(AstroDbContext context)
        {
            _context = context;
        }

        //GET api/Calendar
        [HttpGet]
        public IEnumerable<CalendarItem> Get()
        {
            return _context.CalendarEvents.AsNoTracking().ToList().Select(t => (CalendarItem)t);
        }

        //GET api/Calendar/1
        [HttpGet("{id}")]
        public CalendarItem Get(int id)
        {
            return (CalendarItem)_context.CalendarEvents.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        //POST api/Calendar
        [HttpPost]
        public ObjectResult Post([FromForm]CalendarItem item)
        {
            CalendarEvent calendarEvent = (CalendarEvent)item;
            
            _context.CalendarEvents.Add(calendarEvent);
            _context.SaveChanges();

            return Ok(new
            {
                tid = calendarEvent.Id,
                action = "inserted"
            });
        }

        //PUT api/Calendar/1
        [HttpPut("{id}")]
        public ObjectResult Put(int id, [FromForm] CalendarItem item)
        {
            CalendarEvent calendarEvent = (CalendarEvent)item;

            CalendarEvent oldCalendarEvent = _context.CalendarEvents.FirstOrDefault(t => t.Id == id);

            oldCalendarEvent.Name = calendarEvent.Name;
            oldCalendarEvent.StartDate = calendarEvent.StartDate;
            oldCalendarEvent.EndDate = calendarEvent.EndDate;

            _context.CalendarEvents.Update(oldCalendarEvent);
            _context.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }

        //DELETE api/Calendar/1
        [HttpDelete("{id}")]
        public ObjectResult Delete(int id)
        {
            CalendarEvent calendarEvent = _context.CalendarEvents.FirstOrDefault(t => t.Id == id);

            if(calendarEvent != null)
            {
                _context.CalendarEvents.Remove(calendarEvent);
                _context.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }
    }
}