using Sorteio.Aplicacao.Familias.Pessoas.Dtos;

namespace Sorteio.Aplicacao.Familias.Pessoas.Consultas;

public interface IConsultaPessoa
{
    Task<PessoaDto> ObterPorIdAsync(int id);
    Task<IEnumerable<PessoaDto>> ObterTodosAsync();
}