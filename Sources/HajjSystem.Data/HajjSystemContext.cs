using Microsoft.EntityFrameworkCore;
using HajjSystem.Models.Entities;

namespace HajjSystem.Data;

public class HajjSystemContext : DbContext
{
    public HajjSystemContext(DbContextOptions<HajjSystemContext> options)
        : base(options)
    {
    }

    public DbSet<Registration> Registrations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Company> Companies { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HajjSystemContext).Assembly);
    }
}
