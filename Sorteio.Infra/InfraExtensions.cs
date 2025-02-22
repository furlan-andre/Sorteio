using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Infra.Pessoas;

namespace Sorteio.Infra;

public static class InfraExtensions
{
    public static IServiceCollection AdicionarInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BancoDadosContexto>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Sorteio")));

        services.AddScoped<IPessoaRepository, PessoaRepository>();

        return services;
    }
}