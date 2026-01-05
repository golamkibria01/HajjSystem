namespace HajjSystem.Models.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    ICollection<UserRole> UserRoles { get; set; }
}
