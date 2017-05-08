using System;
using System.Collections.Generic;
using System.Linq;
using Demo02.ApiModels;
using Demo02.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo02.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
		private readonly Context _appContext;

	    public ProductsController(Context appContext)
	    {
		    this._appContext = appContext;
	    }

	    [HttpGet]
		[HttpHead]
        public IActionResult Get()
	    {
		    List<Product> products = _appContext.Product.Include(x => x.Tags).ToList();

			return Ok(products);
        }

        [HttpGet("{id}",  Name = "GetProduct")]
        public IActionResult Get(Guid id)
        {
	        Product product = _appContext.Product.Include(x => x.Tags).FirstOrDefault(x => x.ProductId == id);

	        if (product == null)
	        {
		        return NotFound();
	        }

			return Ok(product);
		}

        [HttpPost]
        public IActionResult Post([FromBody]Product model)
        {
	        if (model == null)
	        {
		        return BadRequest();
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

	        _appContext.Product.Add(model);
	        _appContext.SaveChanges();

	        return CreatedAtRoute("GetProduct", new { id = model.ProductId }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Product model)
        {
	        var product = _appContext.Product.Find(id);
	        if (product == null)
	        {
		        return NotFound();
	        }

	        product.Title = model.Title;

	        try
	        {
		        _appContext.SaveChanges();
	        }
	        catch (Exception ex)
	        {
		        return BadRequest(ex.Message);
	        }

	        return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
	        var product = _appContext.Product.Find(id);
	        if (product == null)
	        {
		        return NotFound();
	        }

	        _appContext.Product.Remove(product);
	        _appContext.SaveChanges();

		    return NoContent();
        }

	    [HttpPatch("{id}")]
	    public IActionResult Patch(Guid id, [FromBody]JsonPatchDocument<Product> patchDocument)
	    {
		    if (patchDocument == null)
		    {
			    return BadRequest();
		    }

		    var product = _appContext.Product.Find(id);
		    if (product == null)
		    {
			    return NotFound();
		    }

			patchDocument.ApplyTo(product);
		    _appContext.SaveChanges();

		    return Ok();
	    }

		[HttpGet("{id}/tags")]
		public IActionResult GetProductTags(Guid id)
		{
			var product = _appContext.Product.Include(x => x.Tags).FirstOrDefault(x => x.ProductId == id);

			if (product == null)
			{
				return NotFound();
			}

			return Ok(product.Tags);
		}

		[HttpOptions("{id}/tags")]
		public IActionResult GetProductTagsOptions(Guid id)
		{
			Response.Headers.Add("Allow",  "GET,OPTIONS");

			return Ok();
		}
	}
}
