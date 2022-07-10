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
}
