using System;
using System.Collections.Generic;
using Demo02.Lib.Entities;

namespace Demo02.Lib.ApiModels.Products
{
    public class ProductApiModel : BaseProductApiModel
    {
	    public Guid ProductId { get; set; }
	    public List<Tag> Tags { get; set; }
    }
}
