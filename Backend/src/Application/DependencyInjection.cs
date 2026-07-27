using Application.Manutencoes.Create;
using Application.Manutencoes.Delete;
using Application.Manutencoes.GetAll;
using Application.Manutencoes.GetById;
using Application.Manutencoes.Update;
using Application.RegistrosOdometro.Create;
using Application.RegistrosOdometro.Delete;
using Application.RegistrosOdometro.GetAll;
using Application.RegistrosOdometro.GetById;
using Application.RegistrosOdometro.Update;
using Application.Usuarios.Create;
using Application.Usuarios.Login;
using Application.Veiculos.Create;
using Application.Veiculos.Delete;
using Application.Veiculos.GetAll;
using Application.Veiculos.GetById;
using Application.Veiculos.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVeiculoService>();
        services.AddScoped<UpdateVeiculoService>();
        services.AddScoped<DeleteVeiculoService>();
        services.AddScoped<GetVeiculoByIdService>();
        services.AddScoped<GetAllVeiculosService>();
        services.AddScoped<CreateUsuarioService>();
        services.AddScoped<LoginUsuarioService>();
        services.AddScoped<CreateRegistroOdometroService>();
        services.AddScoped<UpdateRegistroOdometroService>();
        services.AddScoped<DeleteRegistroOdometroService>();
        services.AddScoped<GetRegistroOdometroByIdService>();
        services.AddScoped<GetAllRegistrosOdometroService>();
        services.AddScoped<CreateManutencaoService>();
        services.AddScoped<UpdateManutencaoService>();
        services.AddScoped<DeleteManutencaoService>();
        services.AddScoped<GetManutencaoByIdService>();
        services.AddScoped<GetAllManutencoesService>();
    }
}
