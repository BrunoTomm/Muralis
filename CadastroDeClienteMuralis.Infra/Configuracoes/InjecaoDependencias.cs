using CadastroDeClienteMuralis.Aplicacao.Interfaces;
using CadastroDeClienteMuralis.Aplicacao.Map;
using CadastroDeClienteMuralis.Aplicacao.Servicos;
using CadastroDeClienteMuralis.Aplicacao.Validators;
using CadastroDeClienteMuralis.Dominio.Mediador.Handlers;
using CadastroDeClienteMuralis.Dominio.Repositorios;
using CadastroDeClienteMuralis.Infra.Configuracoes;
using CadastroDeClienteMuralis.Infra.Interfaces;
using CadastroDeClienteMuralis.Infra.Repositorios;
using CadastroDeClienteMuralis.Infra.Repositorios.Base;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroDeClienteMuralis.Infra.Configuracoes
{
    public static class InjecaoDependencias
    {
        public static IServiceCollection AddInjecaoDependencias(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));

            services.AddHttpClient<IViaCepApiService, ViaCepApiService>();

            services.AddMediatR(typeof(ClienteCommandHandler).Assembly);

            return services;
        }
    }
}
