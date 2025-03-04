namespace Sorteio.Aplicacao.Familias.Pessoas.Dtos;

public class AtualizadorPessoaDto
{
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public DateTime? DataNascimento { get; set; }
    public float? Renda { get; set; }
    
    public AtualizadorPessoaDto() { }
}