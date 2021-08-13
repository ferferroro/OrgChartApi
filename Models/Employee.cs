using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
// using System.Text.Json.Serialization;

public class Employee
{
    // Actual fields
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    // public string Username { get; set; }
    // public string Password { get; set; }

    // // validation field
    // [NotMapped] 
    // public string ConfirmPassword { internal get; set; }


    // Search filter fields
    [NotMapped]
    public long FindById { internal get; set; }

    [NotMapped]
    public string[] FindByIdList { internal get; set; }

    [NotMapped]
    public string[] FindByNotId { internal get; set; }

    [NotMapped]
    public string[] FindByNotIdList { internal get; set; }

    [NotMapped]
    public string FindByFirstName { internal get; set; }

    [NotMapped]
    public string FindByLastName { internal get; set; }

    // [NotMapped]
    // public string FindByUsername { internal get; set; }

    [NotMapped]
    public int FindByPageSize { internal get; set; }

    [NotMapped]
    public int FindByPageNumber { internal get; set; }

    [NotMapped]
    public string SortOrder { internal get; set; }

    [NotMapped]
    public string SortField { internal get; set; }
}

