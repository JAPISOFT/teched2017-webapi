using System.ComponentModel.DataAnnotations;

namespace Demo02.Lib.ApiModels.Products
{
	public class ProductApiModelCreate : BaseProductApiModel
	{
		[Required(ErrorMessage = "Titulek je povinn�")]
		public override string Title { get; set; }
	}
}