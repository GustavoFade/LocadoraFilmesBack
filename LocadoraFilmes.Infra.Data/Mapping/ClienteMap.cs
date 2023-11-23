using LocadoraFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraFilmes.Infra.Data.Mapping
{
    class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("INT")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Cpf)
               .HasColumnName("Cpf")
               .HasColumnType("VARCHAR")
               .IsRequired()
               .HasMaxLength(14);

            builder.Property(x => x.Senha)
               .HasColumnType("VARCHAR")
               .IsRequired()
               .HasMaxLength(350)
               .HasColumnName("Senha");

            builder.Property(x => x.DataCriacao)
              .HasColumnType("DATETIME")
              .HasColumnName("DataCriacao")
              .HasDefaultValueSql("GETDATE()")
              .ValueGeneratedOnAdd();

            builder.HasIndex(x => x.Id, "IX_Id_Cliente");
            builder.HasIndex(x => x.Cpf, "IX_Cpf_Cliente");
        }
    }
}
