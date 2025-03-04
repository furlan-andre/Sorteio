using Sorteio.Aplicacao.Familias.Pessoas.Dtos;

namespace Sorteio.Aplicacao.Familias.Dtos;

public class GerenciaFamiliaDto
{
    public ArmazenaPessoaDto Responsavel { get; set; }
    public ArmazenaPessoaDto? Conjuge { get; set; }
    public List<ArmazenaPessoaDto>? Dependentes { get; set; } = new List<ArmazenaPessoaDto>();
}