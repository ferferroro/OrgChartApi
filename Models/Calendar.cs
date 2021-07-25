using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
public class Calendar
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<CalendarEvent> CalendarEvent { get; set; }
}