using System.ComponentModel.DataAnnotations;

namespace HajjSystem.Models.Models;

public class CompanyUpdateModel
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string CompanyName { get; set; } = string.Empty;
    
    [Required]
    public string CrNumber { get; set; } = string.Empty;
    
    public string Address { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string VatRegNumber { get; set; } = string.Empty;
}
