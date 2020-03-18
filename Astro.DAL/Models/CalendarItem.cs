using System;

namespace Astro.DAL.Models
{
    /// <summary>
    /// Class pass to view with calendar
    /// </summary>
    public class CalendarItem
    {
        public int id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

        public static explicit operator CalendarItem(CalendarEvent ev)
        {
            return new CalendarItem
            {
                id = ev.Id,
                text = ev.Name,
                start_date = ev.StartDate.ToString("yyyy-MM-dd HH:mm"),
                end_date = ev.EndDate.ToString("yyyy-MM-dd HH:mm")
            };
        }

        public static explicit operator CalendarEvent(CalendarItem ev)
        {
            return new CalendarEvent
            {
                Id = ev.id,
                Name = ev.text,
                StartDate = DateTime.Parse(ev.start_date,
                    System.Globalization.CultureInfo.InvariantCulture),
                EndDate = DateTime.Parse(ev.end_date,
                    System.Globalization.CultureInfo.InvariantCulture)
            };
        }
    }
}
