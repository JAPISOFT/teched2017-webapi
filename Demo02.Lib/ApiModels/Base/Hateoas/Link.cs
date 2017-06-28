using System;
using System.Collections.Generic;
using System.Text;

namespace Demo02.Lib.ApiModels.Base.Hateoas
{
    public class Link
    {
	    public const string MethodGet = "GET";
	    public const string MethodPost = "POST";
	    public const string MethodPut = "PUT";
	    public const string MethodDelete = "DELETE";
	    public const string MethodPatch = "PATCH";

		public string Href { get; set; }
		public string Rel { get; set; }
	    public string Method { get; set; } = MethodGet;
    }
}
