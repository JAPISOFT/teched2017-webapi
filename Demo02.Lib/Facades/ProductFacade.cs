using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo02.Lib.ApiModels.Products;
using Demo02.Lib.Entities;
using Demo02.Lib.Repositories;

namespace Demo02.Lib.Facades
{
    public class ProductFacade
    {
	    private readonly UnitOfWork _unitOfWork;

	    public ProductFacade(UnitOfWork unitOfWork)
	    {
		    _unitOfWork = unitOfWork;
	    }

	    public async Task<List<ProductApiModel>> GetProducts()
	    {
		    var products = await _unitOfWork.Products.GetAllWithTags();
		    var result = products.Select(product => new ProductApiModel
		    {
			    ProductId = product.ProductId,
			    Title = product.Title,
				Tags = product.Tags.Select(tag => new TagApiModel
				{
					ProductId = tag.ProductId,
					Name =tag.Name,
					TagId = tag.TagId
				}).ToList()
		    }).ToList();

		    return result;
	    }

	    public async Task<ProductApiModel> GetProduct(Guid id)
	    {
		    var product = await _unitOfWork.Products.GetById(id);
		    var result = product != null
			    ? new ProductApiModel
			    {
				    ProductId = product.ProductId,
				    Title = product.Title,
					Tags = product.Tags.Select(tag => new TagApiModel
					{
						ProductId = tag.ProductId,
						Name =tag.Name,
						TagId = tag.TagId
					}).ToList()
			    }
			    : null;

		    return result;
	    }

	    public async Task<List<TagApiModel>> GetProductTags(Guid productId)
	    {
		    var tags = await _unitOfWork.Tags.GetByProductId(productId);

		    return tags.Select(tag => new TagApiModel
		    {
				ProductId = tag.ProductId,
				Name = tag.Name,
				TagId = tag.TagId
		    }).ToList();
	    }

	    public async Task<ProductApiModel> CreateProduct(ProductApiModelCreate model)
	    {
		    var product = new Product
		    {
			    Title = model.Title
		    };

			_unitOfWork.Products.Insert(product);
			_unitOfWork.Save();

		    return await GetProduct(product.ProductId);
	    }
    }
}
