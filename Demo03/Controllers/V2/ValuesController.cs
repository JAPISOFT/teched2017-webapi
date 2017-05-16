using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Demo03.Controllers.V2
{
	[ApiVersion("2.0")]
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1 v2", "value2 v2" };
		}
	}
}