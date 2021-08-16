using System.Collections.Generic;
public class Calendar : SearchFilters
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<CalendarEvent> CalendarEvent { get; set; }
}