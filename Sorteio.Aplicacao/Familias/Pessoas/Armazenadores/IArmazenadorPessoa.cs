using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Aplicacao.Familias.Pessoas.Armazenadores;

public interface IArmazenadorPessoa
{
   Task<Pessoa> ArmazenarAsync(ArmazenaPessoaDto pessoaDto);
}