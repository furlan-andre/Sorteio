using Sorteio.Aplicacao.Familias.Pessoas.Dtos;

namespace Sorteio.Aplicacao.Familias.Armazenadores;

public interface IArmazenadorDependente
{
    Task<PessoaDto> ArmazenarDependente(int familiaId, ArmazenaPessoaDto armazenaPessoaDto);
}