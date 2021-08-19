using System.ComponentModel.DataAnnotations.Schema;

public class WorkStatuses : SearchFilters
{
    public long Id { get; set; }
    

    [ForeignKey("WorkStatusTemplateId")]
    public WorkStatusTemplate WorkStatusTemplate { get; set; }
    public long WorkStatusTemplateId { get; set; }


    [ForeignKey("WorkStatusId")]
    public WorkStatus WorkStatus { get; set; }
    public long WorkStatusId { get; set; }
    

}