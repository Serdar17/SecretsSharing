using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SecretsSharing.Persistence.EntityTypeConfiguration;

/// <summary>
/// Class for FileDetail configuration
/// </summary>
public class FileConfiguration : IEntityTypeConfiguration<FileDetails>
{
    /// <summary>
    /// method for entity configuration
    /// </summary>
    /// <param name="builder">builder for fluent api configuration</param>
    public void Configure(EntityTypeBuilder<FileDetails> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("files");
    }
}