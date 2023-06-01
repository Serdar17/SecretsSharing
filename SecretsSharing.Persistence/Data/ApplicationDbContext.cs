using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SecretsSharing.Persistence.Interfaces;
using File = Domain.Models.File;

namespace SecretsSharing.Persistence.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder) =>
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    public DbSet<ApplicationUser> Users { get; set; } = null!;

    public DbSet<File> Files { get; set; } = null!;
}