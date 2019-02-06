using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Core.Domain;


namespace Products.Data.Mapping
{
	public class BrandMap
	{
		public BrandMap(EntityTypeBuilder<Brand> entityTypeBuilder)
		{

			//sequence
			
			entityTypeBuilder.Property(t => t.Id).ForSqlServerUseSequenceHiLo("brand_hilo").IsRequired();
			entityTypeBuilder.Property(t => t.Name).IsRequired();
			

		}

	}
}
