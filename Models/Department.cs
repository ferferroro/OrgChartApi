using System.ComponentModel.DataAnnotations;
public class Department
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    public string Name { get; set; }
}