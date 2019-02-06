using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Core.Domain;

namespace Products.Data.Mapping
{
	public class ProductMap
	{
		public ProductMap(EntityTypeBuilder<Product> entityTypeBuilder)
		{
			entityTypeBuilder.Property(t => t.Id).ForSqlServerUseSequenceHiLo("product_hilo").IsRequired();
			entityTypeBuilder.Property(t => t.Name).IsRequired(true);
			entityTypeBuilder.Property(t => t.Description).IsRequired(false);
			entityTypeBuilder.Property(t => t.Price).IsRequired(true).HasColumnType("decimal(18,4)");
			entityTypeBuilder.Property(t => t.PictureUrl).IsRequired(false);
			entityTypeBuilder.HasOne(t => t.Category).WithMany().HasForeignKey(t=>t.CategoryId);
			entityTypeBuilder.HasOne(t => t.Brand).WithMany().HasForeignKey(t => t.BrandId);

		}
	}
}
