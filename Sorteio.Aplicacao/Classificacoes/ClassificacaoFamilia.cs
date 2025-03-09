using Sorteio.Aplicacao.CalculadoraPontuacaoFamiliares;
using Sorteio.Aplicacao.Classificacoes.Dtos;
using Sorteio.Dominio.Familias;

namespace Sorteio.Aplicacao.Classificacoes;

public class ClassificacaoFamilia : IClassificacaoFamilia
{
    private readonly short _quantidadeSorteada = 5;
    private readonly IFamiliaRepository _familiaRepository;
    private readonly ICalculadoraPontuacaoFamiliar _calculadoraPontuacaoFamiliar;
    
    public ClassificacaoFamilia(IFamiliaRepository familiaRepository, ICalculadoraPontuacaoFamiliar calculadoraPontuacaoFamiliar)
    {
        _familiaRepository = familiaRepository;
        _calculadoraPontuacaoFamiliar = calculadoraPontuacaoFamiliar;
    }

    public async Task<List<ClassificacaoFamiliaDto>> RealizarClassificacao()
    {
        List<ClassificacaoFamiliaDto> classificacaoFamiliaDtos = new List<ClassificacaoFamiliaDto>();
        var familias = await _familiaRepository.ObterTodosAsync();
        
        foreach (var familia in familias)
        {
            var pontuacao = _calculadoraPontuacaoFamiliar.Calcular(familia);
            classificacaoFamiliaDtos.Add(new ClassificacaoFamiliaDto
            {
                FamiliaId = familia.Id,
                CpfResponsavel = familia.Responsavel.Cpf,
                NomeResponsavel = familia.Responsavel.Nome,
                DataNascimentoResponsavel = familia.Responsavel.DataNascimento,
                Pontuacao = pontuacao
            });
        }
        
        var classificados = classificacaoFamiliaDtos
            .OrderByDescending(classificacao => classificacao.Pontuacao)
            .ThenBy(classificacao => classificacao.DataNascimentoResponsavel).Take(_quantidadeSorteada)
            .ToList();

        return classificados;
    }
}