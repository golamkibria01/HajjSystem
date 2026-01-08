using System.ComponentModel.DataAnnotations;

namespace HajjSystem.Models.Models;

public class LoginModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public int SeasonId { get; set; }
}
