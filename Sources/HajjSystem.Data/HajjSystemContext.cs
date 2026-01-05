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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HajjSystemContext).Assembly);
    }
}
