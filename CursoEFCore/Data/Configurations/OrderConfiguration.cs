using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(p => p.Status).HasConversion<string>();
            builder.Property(p => p.FreightType).HasConversion<int>();
            builder.Property(p => p.Observation).HasColumnType("VARCHAR(512)");

            builder.HasMany(p => p.Items)
            .WithOne(p => p.Order)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}