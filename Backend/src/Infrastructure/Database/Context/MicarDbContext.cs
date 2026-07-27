using Domain.RegistrosOdometro;
using Domain.Usuarios;
using Domain.Veiculos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Context;

public class MicarDbContext : DbContext
{
    public MicarDbContext(DbContextOptions<MicarDbContext> options) : base(options)
    {
    }

    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<RegistroOdometro> RegistrosOdometro => Set<RegistroOdometro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MicarDbContext).Assembly);
        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum>().HaveConversion<string>();
    }
}
