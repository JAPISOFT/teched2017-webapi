using System.Collections.Generic;

namespace Demo02.Lib.ApiModels.Base.Hateoas
{
	public abstract class BaseHateoasApiModel
	{
		public List<Link> Links { get; set; }
	}
}