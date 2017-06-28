using System.Collections.Generic;

namespace Demo02.Lib.ApiModels.Base
{
	public class Collection<T> where T : class
	{
		public int Offset { get; set; }
		public int Limit { get; set; }
		public int Total { get; set; }
		public int Count { get; set; }
		public List<T> Items { get; set; }
	}
}