using System.Collections.Generic;

namespace Demo02.Lib.ApiModels.Base.Hateoas
{
	public abstract class BaseHateoasApiModel
	{
		protected BaseHateoasApiModel()
		{
			Links = new List<Link>();
		}

		public List<Link> Links { get; set; }
	}
}