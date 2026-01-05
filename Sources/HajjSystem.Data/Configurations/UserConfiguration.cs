using HajjSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HajjSystem.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.MiddleName)
            .HasMaxLength(100);
        
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.Property(u => u.CompanyId);
        
        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(u => u.UserType)
            .IsRequired()
            .HasConversion<int>();
        
        builder.Property(u => u.Address)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(u => u.City)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Country)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Passport)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(u => u.PassportValidity)
            .IsRequired();
        
        builder.Property(u => u.Mobile)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.Property(u => u.SeasonId);
        
        // Add unique constraint on Username
        builder.HasIndex(u => u.Username)
            .IsUnique();
        
        // Add unique constraint on Email
        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}
