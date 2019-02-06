using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Model.ApiResponse
{
	public class ApiResponse<TEntity> where TEntity : class
	{
		public int Page { get; set; }
		public int Limit { get; set; }
		public int Count { get; set; }
		public IEnumerable<TEntity> Data { get;  set; }

		public ApiResponse(int page,int limit,int count,IEnumerable<TEntity> Data)
		{
			this.Page = page;
			this.Limit = limit;
			this.Count = count;
			this.Data = Data;
		}


	}
}
