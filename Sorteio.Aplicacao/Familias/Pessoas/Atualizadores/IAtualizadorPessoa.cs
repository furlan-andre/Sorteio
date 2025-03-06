using Sorteio.Aplicacao.Familias.Pessoas.Dtos;

namespace Sorteio.Aplicacao.Familias.Pessoas.Atualizadores;

public interface IAtualizadorPessoa
{
    Task AtualizarAsync(int id, AtualizaPessoaDto atualizaPessoaDto);
}