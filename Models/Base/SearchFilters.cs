using System.ComponentModel.DataAnnotations.Schema;

public class SearchFilters
{
    // shared temporary fields for retrieve functionality 
    [NotMapped]
    public long FindById { internal get; set; }
    [NotMapped]
    public long[] FindByIdList { internal get; set; }

    [NotMapped]
    public long FindByNotId { internal get; set; }

    [NotMapped]
    public long[] FindByNotIdList { internal get; set; }

    [NotMapped]
    public string FindByName { internal get; set; }

    [NotMapped]
    public string FindByFirstName { internal get; set; }

    [NotMapped]
    public string FindByLastName { internal get; set; }

    [NotMapped]
    public long FindByCalendarId { internal get; set; }

    [NotMapped]
    public long FindByPayrollId { internal get; set; }
    
    [NotMapped]
    public long FindWorkStatusTemplateId { internal get; set; }

    [NotMapped]
    public int FindByPageSize { internal get; set; }

    [NotMapped]
    public int FindByPageNumber { internal get; set; }

    [NotMapped]
    public string SortOrder { internal get; set; }

    [NotMapped]
    public string SortField { internal get; set; }
}