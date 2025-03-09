using System.Collections.Concurrent;
using Sorteio.Aplicacao.Familias.Dtos;
using Sorteio.Aplicacao.Familias.Mappers;
using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Aplicacao.Familias.Pessoas.Mappers;
using Sorteio.Dominio.Familias;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Aplicacao.Familias.Consultas;

public class ConsultaFamilia : IConsultaFamilia
{
    private readonly IFamiliaRepository _familiaRepository;

    public ConsultaFamilia(IFamiliaRepository familiaRepository)
    {
        _familiaRepository = familiaRepository;
    }

    public async Task<FamiliaDto> ObterPorIdAsync(int id)
    {
        var familia = await _familiaRepository.ObterPorIdAsync(id);
        if(familia == null) throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.NaoFoiEncontrada, "familia"));

        return FamiliaMapper.ParaDto(familia);
    }
    
    public async Task<IEnumerable<FamiliaDto>> ObterTodosAsync()
    {
        var familiasDtos = new List<FamiliaDto>();
        var familias = await _familiaRepository.ObterTodosAsync();
        
        var tasks = familias.Select(async familia =>
        {
            familiasDtos.Add(new FamiliaDto()
            {
                Id = familia.Id,
                Responsavel = PessoaMapper.ParaDto(familia.Responsavel),
                Conjuge = familia.Conjuge != null ? PessoaMapper.ParaDto(familia.Conjuge): null,
                Dependentes = await ObterDependentes(familia)
            });
        });
        await Task.WhenAll(tasks);
        
        return familiasDtos;
    }

    private async Task<List<PessoaDto>> ObterDependentes(Familia familia)
    {
        var dependenteDtos = new ConcurrentBag<PessoaDto>();
        var dependentes = (await _familiaRepository.ObterDependentesPorFamiliaIdAsync(familia.Id)).ToList();
        Parallel.ForEach(dependentes, dependente =>
        {
            dependenteDtos.Add(PessoaMapper.ParaDto(dependente));
        });
        
        return dependenteDtos.ToList();
    }
}