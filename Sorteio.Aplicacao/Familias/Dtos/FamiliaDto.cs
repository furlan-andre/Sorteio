using Sorteio.Aplicacao.Familias.Pessoas.Dtos;

namespace Sorteio.Aplicacao.Familias.Dtos;

public class FamiliaDto
{
    public int Id { get; set; }
    public float RendaFamiliar { get; set; }
    public PessoaDto Responsavel { get; set; }
    public PessoaDto? Conjuge { get; set; }
    public List<PessoaDto>? Dependentes { get; set; }
}