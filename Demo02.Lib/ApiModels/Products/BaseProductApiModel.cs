using System.Collections.Generic;
using Demo02.Lib.ApiModels.Base.Hateoas;

namespace Demo02.Lib.ApiModels.Products
{
    public abstract class BaseProductApiModel : BaseHateoasApiModel
    {
	    public virtual string Title { get; set; }
    }
}
