using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
public class Calendar
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    // [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
    public string Name { get; set; }

    public List<CalendarEvent> CalendarEvent { get; set; }
}