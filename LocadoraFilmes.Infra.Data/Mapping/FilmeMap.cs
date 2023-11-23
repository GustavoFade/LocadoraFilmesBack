using LocadoraFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace LocadoraFilmes.Infra.Data.Mapping
{
    class FilmeMap : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.ToTable("Filme");

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
               .HasMaxLength(200);

            builder.Property(x => x.DataCriacao)
               .HasColumnType("DATETIME")
               .HasColumnName("DataCriacao")
               .HasDefaultValueSql("GETDATE()")
               .ValueGeneratedOnAdd();

            builder.Property(x => x.Ativo)
               .HasColumnType("BIT")
               .HasColumnName("FlgAtivo");

            builder
                .HasMany(e => e.Generos)
                .WithMany(e => e.Filme)
                .UsingEntity<Dictionary<string, object>>("FilmeGenero",
                    l => l.HasOne<Genero>().WithMany().HasForeignKey("IdGenero"),
                    r => r.HasOne<Filme>().WithMany().HasForeignKey("IdFilme")
                );

            builder.HasIndex(x => x.Id, "IX_Id_Filme");
        }
    }
}
