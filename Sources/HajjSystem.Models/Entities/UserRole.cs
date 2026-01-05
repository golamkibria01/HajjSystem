using System.ComponentModel.DataAnnotations.Schema;

namespace HajjSystem.Models.Entities;

public class UserRole
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }

    // Navigation properties
    [ForeignKey("UserId")]
    public User? User { get; set; }
    
    [ForeignKey("RoleId")]
    public Role? Role { get; set; }
}
