using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo02.Lib.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo02.Lib.Repositories
{
	public class TagRepository
	{
		private readonly DemoContext _context;
		private readonly DbSet<Tag> _set;

		public TagRepository(DemoContext context)
		{
			_context = context;
			_set = _context.Set<Tag>();
		}

		public async Task<List<Tag>> GetByProductId(Guid productId)
		{
			var result = await _set.Where(x => x.ProductId == productId).ToListAsync();

			return result;
		}
	}
}