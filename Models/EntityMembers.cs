using System.ComponentModel.DataAnnotations.Schema;

public class EntityMembers : SearchFilters
{
    public long Id { get; set; }

    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }
    public long? EmployeeId { get; set; }
    

    [ForeignKey("CompanyId")]
    public Company Company { get; set; }
    public long? CompanyId { get; set; }

    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }
    public long? DepartmentId { get; set; }

    [ForeignKey("TeamId")]
    public Team Team { get; set; }
    public long? TeamId { get; set; }

    [ForeignKey("SubTeamId")]
    public SubTeam SubTeam { get; set; }
    public long? SubTeamId { get; set; }
    

}