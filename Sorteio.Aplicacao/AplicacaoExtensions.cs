using Microsoft.Extensions.DependencyInjection;
using Sorteio.Aplicacao.Classificacoes;
using Sorteio.Aplicacao.Familias.Armazenadores;
using Sorteio.Aplicacao.Familias.Consultas;
using Sorteio.Aplicacao.Familias.Pessoas.Armazenadores;
using Sorteio.Aplicacao.Familias.Pessoas.Atualizadores;
using Sorteio.Aplicacao.Familias.Pessoas.Consultas;

namespace Sorteio.Aplicacao;

public static class AplicacaoExtensions
{
    public static IServiceCollection AdicionarAplicacao(this IServiceCollection services)
    {
        services.AddScoped<IConsultaPessoa, ConsultaPessoa>();
        services.AddScoped<IArmazenadorPessoa, ArmazenadorPessoa>();
        services.AddScoped<IAtualizadorPessoa, AtualizadorPessoa>();
        services.AddScoped<IConsultaFamilia, ConsultaFamilia>();
        services.AddScoped<IArmazenadorFamilia, ArmazenadorFamilia>();
        services.AddScoped<IArmazenadorDependente, ArmazenadorDependente>();
        services.AddScoped<IClassificacaoFamilia, ClassificacaoFamilia>();
        
        return services;
    }
}