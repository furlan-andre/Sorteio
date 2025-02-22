using Sorteio.Aplicacao.Pessoas.Dtos;

namespace Sorteio.Aplicacao.Pessoas.Armazenadores;

public interface IArmazenadorPessoa
{
   Task ArmazenarAsync(PessoaDto pessoaDto);
}