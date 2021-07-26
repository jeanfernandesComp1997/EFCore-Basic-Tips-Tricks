using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoEFCore.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.Phone).HasColumnType("CHAR(11)");
            builder.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(p => p.State).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(p => p.City).HasMaxLength(60).IsRequired();

            builder.HasIndex(i => i.Phone).HasDatabaseName("idx_client_phone");
        }
    }
}