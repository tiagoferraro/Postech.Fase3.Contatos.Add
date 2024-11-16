using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postech.Fase3.Contatos.Add.Domain.Entities;

namespace Postech.Fase3.Contatos.Add.Infra.Repository.Mapping;

public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contato");
        builder.HasKey(c => c.ContatoId);
        builder.Property(c => c.ContatoId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .ValueGeneratedNever();
        builder.Property(c => c.Nome).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Telefone).HasMaxLength(15).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(200);
        builder.Property(c => c.DddId).IsRequired();
        builder.Property(c => c.Ativo).IsRequired();
        builder.Property(c => c.DataInclusao).HasColumnType("smalldatetime");

    }
}