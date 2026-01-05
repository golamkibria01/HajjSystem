using HajjSystem.Models.Entities;

namespace HajjSystem.Models.Models;

public class UserCreateModel
{
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int? CompanyId { get; set; }
    public UserRole Role { get; set; }
    public UserType UserType { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Passport { get; set; } = string.Empty;
    public DateTime PassportValidity { get; set; }
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int? SeasonId { get; set; }
}
