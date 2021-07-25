using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class CalendarEvent
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }    
    
    [ForeignKey("CalendarId")]
    public Calendar Calendar { get; set; }
    public long CalendarId { get; set; }
}