using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class CalendarEvent
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; }
    
    [Required(ErrorMessage = "DateFrom is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateFrom { get; set; }
    
    [Required(ErrorMessage = "DateTo is required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateTo { get; set; }    
    
    

    [ForeignKey("CalendarId")]
    public Calendar Calendar { get; set; }

    [Required]
    public long CalendarId { get; set; }
}