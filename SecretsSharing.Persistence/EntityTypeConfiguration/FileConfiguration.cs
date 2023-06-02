using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SecretsSharing.Persistence.EntityTypeConfiguration;

public class FileConfiguration : IEntityTypeConfiguration<FileDetails>
{
    public void Configure(EntityTypeBuilder<FileDetails> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("files");
    }
}