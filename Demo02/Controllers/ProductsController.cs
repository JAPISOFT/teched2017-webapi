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
		public IActionResult Post([FromBody]ProductApiModelCreate model)
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

			return CreatedAtRoute("GetProduct", new { id = Guid.NewGuid() }, model);
		}

		//      [HttpPut("{id}")]
		//      public IActionResult Put(Guid id, [FromBody]Product model)
		//      {
		//       var product = _appContext.Products.Find(id);
		//       if (product == null)
		//       {
		//        return NotFound();
		//       }

		//       product.Title = model.Title;

		//       try
		//       {
		//        _appContext.SaveChanges();
		//       }
		//       catch (Exception ex)
		//       {
		//        return BadRequest(ex.Message);
		//       }

		//       return Ok(model);
		//      }

		//      [HttpDelete("{id}")]
		//      public IActionResult Delete(Guid id)
		//      {
		//       var product = _appContext.Products.Find(id);
		//       if (product == null)
		//       {
		//        return NotFound();
		//       }

		//       _appContext.Products.Remove(product);
		//       _appContext.SaveChanges();

		//    return NoContent();
		//      }

		//   [HttpPatch("{id}")]
		//   public IActionResult Patch(Guid id, [FromBody]JsonPatchDocument<Product> patchDocument)
		//   {
		//    if (patchDocument == null)
		//    {
		//	    return BadRequest();
		//    }

		//    var product = _appContext.Products.Find(id);
		//    if (product == null)
		//    {
		//	    return NotFound();
		//    }

		//	patchDocument.ApplyTo(product);

		//    TryValidateModel(product);
		//    if (!ModelState.IsValid)
		//    {
		//	    return BadRequest(ModelState);
		//    }

		//    _appContext.SaveChanges();

		//    return NoContent();
		//   }

		//[HttpGet("{id}/tags")]
		//public IActionResult GetProductTags(Guid id)
		//{
		//	var product = _appContext.Products.Include(x => x.Tags).FirstOrDefault(x => x.ProductId == id);

		//	if (product == null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(product.Tags);
		//}

		//[HttpOptions("{id}/tags")]
		//public IActionResult GetProductTagsOptions(Guid id)
		//{
		//	Response.Headers.Add("Allow",  "GET,OPTIONS");

		//	return Ok();
		//}
	}
}
