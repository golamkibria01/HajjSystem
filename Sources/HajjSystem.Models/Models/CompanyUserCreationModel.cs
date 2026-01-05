using System.ComponentModel.DataAnnotations;

namespace HajjSystem.Models.Models;

public class CompanyUserCreationModel
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string CompanyName { get; set; }

    [Required]
    public string CrNumber { get; set; }

    [Required]
    public int SeasonId { get; set; }
}
