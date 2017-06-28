using Demo02.Lib.Entities;

namespace Demo02.Lib.Repositories
{
    public class UnitOfWork
    {
	    private readonly DemoContext _demoContext;
	    private readonly ProductRepository _productRepository;

	    public UnitOfWork(DemoContext demoContext, ProductRepository productRepository)
	    {
		    _demoContext = demoContext;
		    _productRepository = productRepository;
	    }

		// ideálně property injection
	    public ProductRepository Products => _productRepository;

	    public void Save()
		{
			_demoContext.SaveChanges();
		}

		public async void SaveAsync()
		{
			await _demoContext.SaveChangesAsync();
		}
	}
}
