using System;
using System.ComponentModel.DataAnnotations;
public class Payroll : SearchFilters
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string CutOffType { get; set; }
    public DateTime CutOffDay1 { get; set; }
    public DateTime CutOffDay2 { get; set; }
    public DateTime CutOffDay3 { get; set; }
    public DateTime CutOffDay4 { get; set; }
    public DateTime PayrollDay1 { get; set; }
    public DateTime PayrollDay2 { get; set; }

}