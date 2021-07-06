using System.ComponentModel.DataAnnotations;
public class Company
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    // [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
    public string Name { get; set; }
}