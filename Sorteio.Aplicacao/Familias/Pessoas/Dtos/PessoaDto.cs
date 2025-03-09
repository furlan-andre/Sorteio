namespace Sorteio.Aplicacao.Familias.Pessoas.Dtos;

public record PessoaDto
{
    public int Id { get; init; }
    public string Nome { get; init; }
    public string Cpf { get; init; }
    public DateTime DataNascimento { get; init; }
    public float? Renda { get; init; }
    public int? FamiliaId { get; init; }
};
