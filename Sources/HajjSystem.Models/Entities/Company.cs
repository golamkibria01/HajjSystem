using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HajjSystem.Models.Entities;

[Index(nameof(CrNumber), IsUnique = true)]
public class Company
{
    public int Id { get; set; }

     [Required]
    public string CompanyName { get; set; } = string.Empty;
    
    [Required]
    public string CrNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string VatRegNumber { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; }
}
