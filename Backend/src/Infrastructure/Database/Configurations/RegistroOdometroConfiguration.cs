using Domain.RegistrosOdometro;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class RegistroOdometroConfiguration : IEntityTypeConfiguration<RegistroOdometro>
{
    public void Configure(EntityTypeBuilder<RegistroOdometro> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.Veiculo)
            .WithMany(v => v.RegistrosOdometro)
            .HasForeignKey(r => r.VeiculoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Manutencao)
            .WithOne(m => m.RegistroOdometro)
            .HasForeignKey<RegistroOdometro>(r => r.ManutencaoId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
