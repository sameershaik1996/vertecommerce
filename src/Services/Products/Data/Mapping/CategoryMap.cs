using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Core.Domain;


namespace Products.Data.Mapping
{
	public class CategoryMap
	{
		public CategoryMap(EntityTypeBuilder<Category> entityTypeBuilder)
		{
			entityTypeBuilder.Property(t => t.Id).ForSqlServerUseSequenceHiLo("category_hilo").IsRequired();
			entityTypeBuilder.Property(t => t.Name).IsRequired();


		}

	}
}
