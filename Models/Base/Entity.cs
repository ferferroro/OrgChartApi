using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

public class Entity : SearchFilters
{
    // shared actual fields
    [ForeignKey("CalendarId")]
    public Calendar Calendar { get; set; }
    public long? CalendarId { get; set; }


    [ForeignKey("PayrollId")]
    public Payroll Payroll { get; set; }
    public long? PayrollId { get; set; }


    [ForeignKey("WorkStatusTemplateId")]
    public WorkStatusTemplate WorkStatusTemplate { get; set; }
    public long? WorkStatusTemplateId { get; set; }

    

}