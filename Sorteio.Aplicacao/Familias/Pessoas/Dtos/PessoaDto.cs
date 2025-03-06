namespace Sorteio.Aplicacao.Familias.Pessoas.Dtos;

public record PessoaDto(int Id, string Nome, string Cpf, DateTime DataNascimento, float Renda = 0);
