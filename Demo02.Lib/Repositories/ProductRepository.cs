using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo02.Lib.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo02.Lib.Repositories
{
    public class ProductRepository
    {
	    private readonly DemoContext _context;
	    private readonly DbSet<Product> _set;

	    public ProductRepository(DemoContext context)
	    {
		    _context = context;
		    _set = context.Products;
	    }

	    public async Task<Product> GetById(Guid id)
	    {
		    return await _set.FindAsync(id);
	    }

	    public async Task<List<Product>> GetAllWithTags()
	    {
		    return await _set.Include(x => x.Tags).ToListAsync();
	    }

	    public async void Insert(Product product)
	    {
		    await _set.AddAsync(product);
	    }

	    public void Update(Product product)
	    {
		    _set.Attach(product);
		    _context.Entry(product).State = EntityState.Modified;
	    }

	    public void Delete(Product product)
	    {
		    if (_context.Entry(product).State == EntityState.Detached)
		    {
			    _set.Attach(product);
		    }

			_set.Remove(product);
	    }
    }
}
