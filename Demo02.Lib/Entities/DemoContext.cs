using Microsoft.EntityFrameworkCore;

namespace Demo02.Lib.Entities
{
    public class DemoContext : DbContext
    {
	    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	    {
		    optionsBuilder.UseInMemoryDatabase("apidemo");
	    }

	    public DbSet<Product> Products { get; set; }
		public DbSet<Tag> Tags { get; set; }
    }
}
