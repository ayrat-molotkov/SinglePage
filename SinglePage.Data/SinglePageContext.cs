using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SinglePage.Data.Models;

namespace SinglePage.Data;

public class SinglePageContext : DbContext
{
    public SinglePageContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source='localhost\\SQLEXPRESS'; Connect Timeout='60'; Initial Catalog='SinglePage'; Persist Security Info='True'; Integrated Security='True';");
    }

    public DbSet<VirtualServer> VirtualServers { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<VirtualServer>(c =>
        {
            c.HasKey(c => c.VirtualServerId);
            c.Property(p => p.RemoveDateTime).HasDefaultValue(null);
        });        
    }
}
