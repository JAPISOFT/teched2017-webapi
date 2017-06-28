using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo02.Lib.Entities
{
    public class Product : IValidatableObject
    {
		public Guid ProductId { get; set; }

		[Required]
		public string Title { get; set; }
		public List<Tag> Tags { get; set; }


	    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	    {
		    if (Title.StartsWith("B"))
		    {
			    yield return new ValidationResult("Title cannot starts with B", new[] {"Title"});
		    }
	    }
    }
}
