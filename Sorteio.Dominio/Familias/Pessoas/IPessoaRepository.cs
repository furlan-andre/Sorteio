using Sorteio.Dominio.Familia.Pessoas;

namespace Sorteio.Dominio.Familias.Pessoas;

public interface IPessoaRepository
{
    Task<IEnumerable<Pessoa>> ObterTodosAsync();
    Task<Pessoa> ObterPorIdAsync(int id);
}