using Sorteio.Aplicacao.Pessoas.Dtos;

namespace Sorteio.Aplicacao.Pessoas;

public interface IConsultaPessoa
{
    Task<PessoaDto> ObterPorIdAsync(int id);
    Task<IEnumerable<PessoaDto>> ObterTodosAsync();
}