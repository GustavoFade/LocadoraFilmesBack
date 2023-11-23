using LocadoraFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraFilmes.Infra.Data.Mapping
{
    class GeneroMap : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Genero");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("INT")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Nome)
               .HasColumnName("Nome")
               .HasColumnType("VARCHAR")
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(x => x.DataCriacao)
               .HasColumnType("DATETIME")
               .HasColumnName("DataCriacao")
               .HasDefaultValueSql("GETDATE()")
               .ValueGeneratedOnAdd();

            builder.Property(x => x.Ativo)
               .HasColumnType("BIT")
               .HasColumnName("FlgAtivo");

            builder.HasIndex(x => x.Id, "IX_Id_Genero");
        }
    }
}
