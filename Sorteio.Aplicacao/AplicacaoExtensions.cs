using Microsoft.Extensions.DependencyInjection;
using Sorteio.Servico.Pessoas;

namespace Sorteio.Aplicacao;

public static class AplicacaoExtensions
{
    public static IServiceCollection AdicionarAplicacao(this IServiceCollection services)
    {
        services.AddScoped<IConsultaPessoa, ConsultaPessoa>();
        return services;
    }
}