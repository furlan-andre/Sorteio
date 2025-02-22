using Sorteio.Servico.Pessoas.Dtos;

namespace Sorteio.Servico.Pessoas;

public interface IConsultaPessoa
{
    Task<PessoaDto> ObterPorIdAsync(int id);
    Task<IEnumerable<PessoaDto>> ObterTodosAsync();
}