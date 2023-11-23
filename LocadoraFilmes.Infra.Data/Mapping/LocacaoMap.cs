using LocadoraFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace LocadoraFilmes.Infra.Data.Mapping
{
    class LocacaoMap : IEntityTypeConfiguration<Locacao>
    {
        public void Configure(EntityTypeBuilder<Locacao> builder)
        {
            builder.ToTable("Locacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("INT")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.CpfCliente)
                .HasColumnName("CpfCliente")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(14);

            builder.Property(x => x.IdFilmesLocacao)
                .HasColumnName("IdFilmesLocacao")
                .HasColumnType("INT");

            builder.Property(x => x.DataLocacao)
               .HasColumnType("DATETIME")
               .HasColumnName("DataLocacao");

            builder
                .HasMany(e => e.FilmesLocacao)
                .WithMany(e => e.Locacoes)
                .UsingEntity<Dictionary<string, object>>("FilmeLocacao",
                    l => l.HasOne<Filme>().WithMany().HasForeignKey("IdFilme").HasPrincipalKey(nameof(Filme.Id)),
                    r => r.HasOne<Locacao>().WithMany().HasForeignKey("IdLocacao").HasPrincipalKey(nameof(Locacao.Id))
                );

            builder.HasOne(x => x.Cliente)
                .WithOne(x => x.Locacao)
                .HasForeignKey<Locacao>(x => x.CpfCliente)
                .HasPrincipalKey<Cliente>(x => x.Cpf);

            builder.HasIndex(x => x.Id, "IX_Id_Locacao");
        }
    }
}
