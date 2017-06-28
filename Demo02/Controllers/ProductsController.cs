using System;
using System.Threading.Tasks;
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

	    [HttpGet]
		[HttpHead]
        public async Task<IActionResult> Get()
	    {
		    var result = await _productFacade.GetProducts();

		    return Ok(result);
	    }

        [HttpGet("{id}",  Name = "GetProduct")]
        public async Task<IActionResult> Get(Guid id)
        {
	        var result = await _productFacade.GetProduct(id);
	        if (result == null)
	        {
		        return NotFound("Produkt nebyl nalezen");
	        }

	        return Ok(result);
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
}
