public class Entity
{
    // shared actual fields
    public long CalendarId { get; set; }
    public long PayrollId { get; set; }
    public long WorkStatusTemplateId { get; set; }


    // shared temporary fields for retrieve functionality 
    public long FindById { internal get; set; }

    public long[] FindByIdList { internal get; set; }

    public long FindByNotId { internal get; set; }

    public long[] FindByNotIdList { internal get; set; }

    public string FindByName { internal get; set; }

    public string FindByFirstName { internal get; set; }

    public string FindByLastName { internal get; set; }

    public long FindByCalendarId { internal get; set; }

    public long FindByPayrollId { internal get; set; }
    
    public long FindWorkStatusTemplateId { internal get; set; }

    public int FindByPageSize { internal get; set; }

    public int FindByPageNumber { internal get; set; }

    public string SortOrder { internal get; set; }

    public string SortField { internal get; set; }
}