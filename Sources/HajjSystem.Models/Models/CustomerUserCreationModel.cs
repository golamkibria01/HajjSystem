using System.ComponentModel.DataAnnotations;

namespace HajjSystem.Models.Models;

public class CustomerUserCreationModel
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public int SeasonId { get; set; }
}
