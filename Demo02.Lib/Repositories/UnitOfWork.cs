using Demo02.Lib.Entities;

namespace Demo02.Lib.Repositories
{
    public class UnitOfWork
    {
	    private readonly DemoContext _demoContext;
	    private readonly ProductRepository _productRepository;
	    private readonly TagRepository _tagRepository;

	    public UnitOfWork(DemoContext demoContext, ProductRepository productRepository, TagRepository tagRepository)
	    {
		    _demoContext = demoContext;
		    _productRepository = productRepository;
		    _tagRepository = tagRepository;
	    }

		// ideálně property injection
	    public ProductRepository Products => _productRepository;
	    public TagRepository Tags => _tagRepository;

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
