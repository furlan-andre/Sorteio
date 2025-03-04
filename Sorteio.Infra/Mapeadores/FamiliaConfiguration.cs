using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sorteio.Dominio.Familias;

namespace Sorteio.Infra.Mapeadores;

public class FamiliaConfiguration : IEntityTypeConfiguration<Familia>
{
    public void Configure(EntityTypeBuilder<Familia> builder)
    {
        builder.ToTable("Familia");
        builder.HasKey(f => f.Id);
        builder.HasOne(f => f.Responsavel)
            .WithMany()
            .IsRequired()
            .HasForeignKey(f => f.ResponsavelId)
            .OnDelete(DeleteBehavior.NoAction); 
        builder.HasOne(f => f.Conjuge)
            .WithMany()
            .HasForeignKey(f => f.ConjugeId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(f => f.Dependentes)
            .WithOne(d => d.Familia)
            .HasForeignKey(p => p.FamiliaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}