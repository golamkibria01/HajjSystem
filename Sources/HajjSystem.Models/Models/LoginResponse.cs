namespace HajjSystem.Models.Models;

public class LoginResponse
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserType { get; set; } = string.Empty;
    public int? SeasonId { get; set; }
    public List<string> Roles { get; set; } = new();
}
