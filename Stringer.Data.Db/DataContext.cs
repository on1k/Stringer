using Microsoft.EntityFrameworkCore;
using Stringer.Data.Domain.Models;

namespace Stringer.Data.Db;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StringEntity>().HasKey(t => t.Id);
        modelBuilder.Entity<StringEntity>().Property(t => t.Id).ValueGeneratedNever();
        modelBuilder.Entity<StringEntity>().HasIndex(t => t.OwnerId).IsUnique(false);
        modelBuilder.Entity<StringEntity>().Property(t => t.Text).HasMaxLength(3000);
    }
    
    public DbSet<StringEntity?> StringEntities { get; set; }
}