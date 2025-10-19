using Microsoft.EntityFrameworkCore;
using newmvc.Models;

namespace newmvc.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasPrecision(10, 2);
    }

    public DbSet<Product> Products { get; set; }
}