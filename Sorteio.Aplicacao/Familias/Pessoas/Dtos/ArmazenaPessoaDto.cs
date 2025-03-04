namespace Sorteio.Aplicacao.Familias.Pessoas.Dtos;

public record ArmazenaPessoaDto(string Nome, string Cpf, DateTime DataNascimento, float Renda = 0);