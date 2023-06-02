using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SecretsSharing.Persistence.Repository.Interfaces;

namespace SecretsSharing.Persistence.Data;

/// <summary>
/// a class for interacting with a database
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }

    /// <summary>
    /// method for entity configuration
    /// </summary>
    /// <param name="builder">builder for fluent api configuration</param>
    protected override void OnModelCreating(ModelBuilder builder) =>
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    /// <summary>
    /// List with all users from database
    /// </summary>
    public DbSet<ApplicationUser> Users { get; set; } = null!;

    /// <summary>
    /// List with all files from database
    /// </summary>
    public DbSet<FileDetails> Files { get; set; } = null!;
}