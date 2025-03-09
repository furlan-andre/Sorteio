using Sorteio.Aplicacao.Classificacoes.Dtos;

namespace Sorteio.Aplicacao.Classificacoes;

public interface IClassificacaoFamilia
{
    Task<List<ClassificacaoFamiliaDto>> RealizarClassificacao();
}