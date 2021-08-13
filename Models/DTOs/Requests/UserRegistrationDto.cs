using System.ComponentModel.DataAnnotations;

namespace OrgChartApi.Models.DTOs.Requests
{
    public class UserRegistrationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string ConfirmPassword { internal get; set; }
    }
}