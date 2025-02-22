using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sorteio.Dominio.Familia.Pessoas;

namespace Sorteio.Infra.Mapeadores;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("Pessoa");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(p => p.Cpf)
            .IsRequired();
        builder.Property(p => p.DataNascimento)
            .HasColumnType("DateTime")
            .IsRequired();
    }
}