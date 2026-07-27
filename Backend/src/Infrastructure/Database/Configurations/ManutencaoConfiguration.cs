using Domain.Manutencoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class ManutencaoConfiguration : IEntityTypeConfiguration<Manutencao>
{
    public void Configure(EntityTypeBuilder<Manutencao> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasOne(m => m.Veiculo)
            .WithMany(v => v.Manutencoes)
            .HasForeignKey(m => m.VeiculoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
