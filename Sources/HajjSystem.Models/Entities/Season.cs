using Microsoft.EntityFrameworkCore;

namespace HajjSystem.Models.Entities
{
    [Index(nameof(Title), IsUnique = true)]
    public class Season
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isCurrent { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
