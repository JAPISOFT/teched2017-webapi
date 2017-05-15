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

        //public IActionResult Get()
        //public IActionResult Get(Guid id)
        //public IActionResult Post([FromBody]Product model)
        //public IActionResult Put(Guid id, [FromBody]Product model)
        //public IActionResult Delete(Guid id)
	    //public IActionResult Patch(Guid id, [FromBody]JsonPatchDocument<Product> patchDocument)
		//public IActionResult GetProductTags(Guid id)
		//public IActionResult GetProductTagsOptions(Guid id)
		//{
		//	Response.Headers.Add("Allow",  "GET,OPTIONS");

		//	return Ok();
		//}
	}
}
