using Contracts.Repositories;
using Contracts.Repositories.RegistrosOdometro;
using Contracts.Repositories.Usuarios;
using Contracts.Repositories.Veiculos;
using Infrastructure.Authentications;
using Infrastructure.Database.Context;
using Infrastructure.Database.Repositories;
using Infrastructure.Database.Repositories.RegistrosOdometro;
using Infrastructure.Database.Repositories.Usuarios;
using Infrastructure.Database.Repositories.Veiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<MicarDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRegistroOdometroRepository, RegistroOdometroRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddAuth(configuration);
    }

    public static void UseInfrastructure(this IHost app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        using MicarDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<MicarDbContext>();

        ILogger logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
            .CreateLogger(nameof(DependencyInjection));

        IEnumerable<string> pendingMigrations = dbContext.Database.GetPendingMigrations().ToList();

        if (!pendingMigrations.Any())
        {
            logger.LogInformation("No pending migrations to apply.");
            return;
        }

        logger.LogInformation(
            "Applying {Count} pending migration(s): {Migrations}",
            pendingMigrations.Count(),
            string.Join(", ", pendingMigrations));

        dbContext.Database.Migrate();

        logger.LogInformation("Migrations applied successfully.");
    }
}
