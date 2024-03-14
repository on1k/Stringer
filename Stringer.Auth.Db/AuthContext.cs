using Microsoft.EntityFrameworkCore;
using Stringer.Auth.Domain.Models;

namespace Stringer.Auth.Db;

public class AuthContext : DbContext
{
    public AuthContext(DbContextOptions<AuthContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(t => t.Id);
        modelBuilder.Entity<User>().Property(t => t.Id).ValueGeneratedNever();
        modelBuilder.Entity<User>().Property(t => t.Login).HasMaxLength(50);
        modelBuilder.Entity<User>().Property(t => t.Password).HasMaxLength(50);
    }
    
    public DbSet<User?> Users { get; set; }

}