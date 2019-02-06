using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.DataStructure
{
	public class ApiList<T> : List<T>
	{

		public int count { get; private set; }

		public int PageIndex { get; private set; }
		public int PageSize { get; private set; }

		public ApiList(IQueryable<T> source, int pageIndex, int pageSize)
		{
			count = source.Count();
			PageSize = pageSize;
			PageIndex = pageIndex;
			AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
		}
	}

}
