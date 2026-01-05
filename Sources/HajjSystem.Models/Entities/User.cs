using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HajjSystem.Models.Entities;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
    public int? CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    public Company? Company { get; set; }

    [Required]
    public UserType UserType { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Passport { get; set; } = string.Empty;
    public DateTime PassportValidity { get; set; }
    public string Mobile { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public int? SeasonId { get; set; }

    [ForeignKey("SeasonId")]
    public Season? Season { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}

public enum UserType
{
    Customer,
    CompanyUser
}
