using System.ComponentModel.DataAnnotations;
public class Department: Entity
{
    public long Id { get; set; }
    
    public string Name { get; set; }
}