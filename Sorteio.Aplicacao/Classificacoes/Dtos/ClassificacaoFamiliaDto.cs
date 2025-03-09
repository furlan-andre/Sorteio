namespace Sorteio.Aplicacao.Classificacoes.Dtos;

public record ClassificacaoFamiliaDto
{
    public int FamiliaId { get; init; }
    public string CpfResponsavel { get; init; }
    public string NomeResponsavel { get; init; }
    public DateTime DataNascimentoResponsavel { get; init; }
    public int Pontuacao { get; init; }
}