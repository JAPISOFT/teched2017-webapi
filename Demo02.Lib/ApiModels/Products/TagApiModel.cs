using System;
using Demo02.Lib.ApiModels.Base.Hateoas;

namespace Demo02.Lib.ApiModels.Products
{
	public class TagApiModel : BaseHateoasApiModel
	{
		public Guid TagId { get; set; }
		public Guid ProductId { get; set; }
		public string Name { get; set; }
	}
}