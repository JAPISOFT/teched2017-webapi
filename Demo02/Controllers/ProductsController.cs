using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo02.Lib.ApiModels.Base.Hateoas;
using Demo02.Lib.ApiModels.Products;
using Demo02.Lib.Facades;
using Microsoft.AspNetCore.Mvc;

namespace Demo02.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ApiController
    {
	    private readonly ProductFacade _productFacade;

	    public ProductsController(ProductFacade productFacade)
	    {
		    _productFacade = productFacade;
	    }

	    [HttpGet(Name = "GetAllProducts")]
		[HttpHead]
        public async Task<IActionResult> Get()
	    {
		    var result = await _productFacade.GetProducts();

		    return Ok(result.AddHateoas(Url));
	    }

        [HttpGet("{id}",  Name = "GetProduct")]
        public async Task<IActionResult> Get(Guid id)
        {
	        var result = await _productFacade.GetProduct(id);
	        if (result == null)
	        {
		        return NotFound("Produkt nebyl nalezen");
	        }

	        return Ok(result.AddHateoas(Url));
		}

	    [HttpGet("{productId}/tags",  Name = "GetProductTags")]
	    public async Task<IActionResult> GetProductTags(Guid productId)
	    {
		    var result = await _productFacade.GetProductTags(productId);

		    return Ok(result.AddHateoas(Url));
	    }

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]ProductApiModelCreate model)
		{
			if (model == null)
			{
				return BadRequest("Data produktu nebyla nalezena");
			}

			string err;
			if (!ValidationService.ValidateTitle(model.Title, out err))
			{
				ModelState.AddModelError("Title", err);
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _productFacade.CreateProduct(model);

			return CreatedAtRoute("GetProduct", new { id = result.ProductId }, result);
		}
	}

	public static class HateoasExtensions
	{
		public static List<ProductApiModel> AddHateoas(this List<ProductApiModel> products, IUrlHelper urlHelper)
		{
			foreach (var product in products)
			{
				product.Links.Add(new Link
				{
					Href = urlHelper.Link("GetProduct", new {id = product.ProductId}),
					Method = "GET",
					Rel = "self",
					Description = "Odkaz na detail produktu"
				});

				product.Tags.AddHateoas(urlHelper);
			}

			return products;
		}

		public static ProductApiModel AddHateoas(this ProductApiModel product, IUrlHelper urlHelper)
		{
			product.Links.Add(new Link
			{
				Href = urlHelper.Link("GetProduct", new { id = product.ProductId }),
				Method = "GET",
				Rel = "self",
				Description = "Odkaz na detail produktu"
			});

			product.Links.Add(new Link
			{
				Href = urlHelper.Link("GetAllProducts", null),
				Method = "GET",
				Rel = "products",
				Description = "Přehled všech produktů"
			});

			product.Tags.AddHateoas(urlHelper);

			return product;
		}

		public static List<TagApiModel> AddHateoas(this List<TagApiModel> tags, IUrlHelper urlHelper)
		{
			foreach (var tag in tags)
			{
				tag.Links.Add(new Link
				{
					Href = urlHelper.Link("GetProductTags", new { productId = tag.ProductId }),
					Method = "GET",
					Rel = "self",
					Description = "Štítky k produktu "  + tag.ProductId
				});

				tag.Links.Add(new Link
				{
					Href = urlHelper.Link("GetProduct", new { id = tag.ProductId }),
					Method = "GET",
					Rel = "parent",
					Description = "Detail produktu "  + tag.ProductId
				});
			}

			return tags;
		}
	}
}
