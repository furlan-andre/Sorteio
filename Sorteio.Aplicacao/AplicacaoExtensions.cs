using Microsoft.Extensions.DependencyInjection;
using Sorteio.Aplicacao.Pessoas;
using Sorteio.Aplicacao.Pessoas.Armazenadores;

namespace Sorteio.Aplicacao;

public static class AplicacaoExtensions
{
    public static IServiceCollection AdicionarAplicacao(this IServiceCollection services)
    {
        services.AddScoped<IConsultaPessoa, ConsultaPessoa>();
        services.AddScoped<IArmazenadorPessoa, ArmazenadorPessoa>();
        
        return services;
    }
}