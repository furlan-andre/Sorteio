using Sorteio.Aplicacao.Familias.Dtos;

namespace Sorteio.Aplicacao.Familias.Consultas;

public interface IConsultaFamilia
{
    Task<IEnumerable<FamiliaDto>> ObterTodosAsync();
    Task<FamiliaDto> ObterPorIdAsync(int id);
}