using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo02.Lib.Entities
{
    public class Product
    {
		public Guid ProductId { get; set; }

		[Required]
		public string Title { get; set; }
		public List<Tag> Tags { get; set; }
    }
}
