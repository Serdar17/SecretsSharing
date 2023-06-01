using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SecretsSharing.Persistence.EntityTypeConfiguration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Email).IsUnique();
        
        builder
            .HasMany(x => x.Files)
            .WithOne(x => x.ApplicationUser)
            .HasForeignKey(x => x.UserId);

        builder.ToTable("users");
    }
}