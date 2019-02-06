using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Core.Domain
{
	//Domain class which represents the Brand table
	public class Brand : BaseEntity
	{
		
		public string Name { get; set; }
	}
}
