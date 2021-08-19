using System.Collections.Generic;

public class WorkStatusTemplate : SearchFilters
{
    public long Id { get; set; }
    public string Name { get; set; }

    public List<WorkStatuses> WorkStatuses { get; set; }

}