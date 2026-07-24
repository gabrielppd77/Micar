using Application.Usuarios.Create;
using Application.Usuarios.Login;
using Application.Veiculos.Create;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVeiculoService>();
        services.AddScoped<CreateUsuarioService>();
        services.AddScoped<LoginUsuarioService>();
    }
}
