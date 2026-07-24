using Application.Users.Create;
using Application.Users.Login;
using Application.Veiculos.Create;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVeiculoService>();
        services.AddScoped<CreateUserService>();
        services.AddScoped<LoginUserService>();
    }
}
