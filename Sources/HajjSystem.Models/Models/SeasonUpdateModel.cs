using System.ComponentModel.DataAnnotations;

namespace HajjSystem.Models.Models;

public class SeasonUpdateModel
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    public bool isCurrent { get; set; }
}
