using Sorteio.Aplicacao.Familias.Dtos;
using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Dominio.Familias;
using Sorteio.Dominio.Familias.Pessoas;
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

        return MontarFamilia(familia);
    }
    
    public async Task<IEnumerable<FamiliaDto>> ObterTodosAsync()
    {
        var familias = await _familiaRepository.ObterTodosAsync();
        var tasks = familias.Select(async familia =>
        {
            var dependentes = await _familiaRepository.ObterDependentesPorFamiliaIdAsync(familia.Id);
            if (dependentes != null)
            {
                familia.AdicionarDependentes(dependentes.ToList());
            }
        });
        await Task.WhenAll(tasks);
        
        return familias.Select(familia => MontarFamilia(familia)).ToList();
    }

    private FamiliaDto MontarFamilia(Familia familia)
    {
        if (familia is null) return null;
        
        return new FamiliaDto()
        {
            Id = familia.Id,
            RendaFamiliar = familia.ObterRendaFamiliar(),
            Responsavel = MontarPessoa(familia.Responsavel),
            Conjuge = MontarPessoa(familia.Conjuge),
            Dependentes = MontarDependentes(familia.Dependentes)
        };
    }

    private PessoaDto MontarPessoa(Pessoa pessoa)
    {
        if (pessoa == null) return null;
        return new PessoaDto
        (
            pessoa.Id,
            pessoa.Nome,
            pessoa.Cpf,
            pessoa.DataNascimento,
            pessoa.Renda
        );
    }
    
    private List<PessoaDto> MontarDependentes(IEnumerable<Pessoa> dependentes)
    {
        if(dependentes == null) return new List<PessoaDto>();
        
        return dependentes.Select(dependente => MontarPessoa(dependente)).ToList();
    }
}