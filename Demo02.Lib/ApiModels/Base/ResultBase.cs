using System.Collections.Generic;
using System.Net;
using Demo02.Lib.ApiModels.Base.Hateoas;

namespace Demo02.Lib.ApiModels.Base
{
	public class ResultBase : BaseHateoasApiModel
	{
		public HttpStatusCode Status { get; set; }
		public string Message { get; set; }
	}
}
