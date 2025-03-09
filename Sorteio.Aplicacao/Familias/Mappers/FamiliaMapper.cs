using Sorteio.Aplicacao.Familias.Dtos;
using Sorteio.Aplicacao.Familias.Pessoas.Mappers;
using Sorteio.Dominio.Familias;
using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Aplicacao.Familias.Mappers;

public static class FamiliaMapper
{
    public static FamiliaDto ParaDto(Familia familia) => new FamiliaDto()
    {
        Id = familia.Id,
        RendaFamiliar = familia.ObterRendaFamiliar(),
        Responsavel = PessoaMapper.ParaDto(familia.Responsavel),
        Dependentes = familia.Dependentes.Select(dependente => PessoaMapper.ParaDto(dependente)).ToList(),
        Conjuge = familia.Conjuge != null ? PessoaMapper.ParaDto(familia.Conjuge): null
    };
}