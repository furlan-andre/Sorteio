using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Dominio.Familias;

public interface IFamiliaRepository
{
    Task<IEnumerable<Familia>> ObterTodosAsync();
    Task<Familia> ObterPorIdAsync(int id);
    Task<IEnumerable<Pessoa>> ObterDependentesPorFamiliaIdAsync(int id);
    Task AdicionarAsync(Familia familia);
}