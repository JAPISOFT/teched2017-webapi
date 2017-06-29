using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Demo02.Lib.ApiModels.Base.Hateoas;

namespace Demo02.Lib.ApiModels.Products
{
    public abstract class BaseProductApiModel : BaseHateoasApiModel, IValidatableObject
    {
	    [Required(ErrorMessage = "Titulek je povinný")]
	    public virtual string Title { get; set; }

	    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	    {
		    if (Title.StartsWith("B"))
		    {
			    yield return new ValidationResult("Title cannot starts with B", new[] {"Title"});
		    }
	    }
    }
}
