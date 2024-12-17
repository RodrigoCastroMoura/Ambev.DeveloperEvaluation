
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.Number).IsRequired().HasMaxLength(50);
        builder.Property(s => s.SaleDate).IsRequired();
        builder.Property(s => s.Customer).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Branch).IsRequired().HasMaxLength(100);
        builder.Property(s => s.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");

        builder.OwnsMany(s => s.Items, items =>
        {
            items.ToTable("SaleItems");
            items.WithOwner().HasForeignKey("SaleId");
            items.HasKey("Id");
            items.Property<Guid>("Id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            items.Property(i => i.ProductId).IsRequired();
            items.Property(i => i.Quantity).IsRequired();
            items.Property(i => i.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            items.Property(i => i.Discount).IsRequired().HasColumnType("decimal(18,2)");
            items.Property(i => i.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
        });
    }
}
