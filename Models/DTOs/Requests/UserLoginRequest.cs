using System.ComponentModel.DataAnnotations;

namespace OrgChartApi.Models.DTOs.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
