using Demo02.ApiModels;
using Microsoft.EntityFrameworkCore;

namespace Demo02.Repositories
{
    public class Context : DbContext
    {
		public DbSet<Product> Product { get; set; }
		public DbSet<Tag> Tags { get; set; }

	    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	    {
		    optionsBuilder.UseInMemoryDatabase("test");
	    }
    }
}
