using System.ComponentModel.DataAnnotations;
public class Employee
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    public string Name { get; set; }
}