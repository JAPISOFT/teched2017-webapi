using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Demo02.Lib.ApiModels.Base;
using Demo02.Lib.ApiModels.Products;
using Demo02.Lib.Repositories;

namespace Demo02.Lib.Facades
{
    public class ProductFacade
    {
	    private readonly UnitOfWork _uow;

	    public ProductFacade(UnitOfWork uow)
	    {
		    this._uow = uow;
	    }

	    public async Task<List<ProductApiModel>> GetProducts()
	    {
		    var products = await _uow.Products.GetAllWithTags();
		    var result = products.Select(p => new ProductApiModel()
		    {
			    ProductId = p.ProductId,
				Title = p.Title
		    }).ToList();

		    return result;
	    }

	    public async Task<ProductApiModel> GetProduct(Guid id)
	    {
		    var product = await _uow.Products.GetById(id);
		    var result = product != null
			    ? new ProductApiModel
			    {
				    ProductId = product.ProductId,
				    Title = product.Title
			    }
			    : null;

		    return result;
	    }
    }
}
