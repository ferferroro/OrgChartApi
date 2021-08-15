using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
// using System.Text.Json.Serialization;

public class Employee : Entity
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

}

