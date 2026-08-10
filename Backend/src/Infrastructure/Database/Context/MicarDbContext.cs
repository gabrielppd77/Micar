using Domain.Common;
using Domain.Manutencoes;
using Domain.RegistrosOdometro;
using Domain.Usuarios;
using Domain.Veiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Database.Context;

public class MicarDbContext : DbContext
{
    public MicarDbContext(DbContextOptions<MicarDbContext> options) : base(options)
    {
    }

    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<RegistroOdometro> RegistrosOdometro => Set<RegistroOdometro>();
    public DbSet<Manutencao> Manutencoes => Set<Manutencao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MicarDbContext).Assembly);
        modelBuilder.HasDefaultSchema(Schemas.Default);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var idProperty = entityType.FindProperty(nameof(Entity.Id));
            if (idProperty is not null)
                idProperty.ValueGenerated = ValueGenerated.Never;
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>().HaveConversion<string>();
    }
}
