using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SecretsSharing.Persistence.EntityTypeConfiguration;

/// <summary>
/// Сlass for user configuration
/// </summary>
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    /// <summary>
    /// method for entity configuration
    /// </summary>
    /// <param name="builder">builder for fluent api configuration</param>
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Email).IsUnique();
        
        // a one-to-many relationship for the user
        builder
            .HasMany(x => x.Files)
            .WithOne(x => x.ApplicationUser)
            .HasForeignKey(x => x.UserId);

        builder.ToTable("users");
    }
}