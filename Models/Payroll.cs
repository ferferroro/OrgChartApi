using System.ComponentModel.DataAnnotations;
public class Payroll
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    public string CutOffType { get; set; }
}