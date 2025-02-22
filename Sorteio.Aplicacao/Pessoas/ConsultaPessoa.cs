using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Servico.Pessoas.Dtos;

namespace Sorteio.Servico.Pessoas;

public class ConsultaPessoa : IConsultaPessoa
{
    private readonly IPessoaRepository _pessoaRepository;

    public ConsultaPessoa(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    public async Task<PessoaDto> ObterPorIdAsync(int id)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id);
        if (pessoa == null) return null;
        
        return new PessoaDto(pessoa.Id, pessoa.Nome, pessoa.Cpf,pessoa.DataNascimento);
    }

    public async Task<IEnumerable<PessoaDto>> ObterTodosAsync()
    {
        var pessoas = await _pessoaRepository.ObterTodosAsync();
        return pessoas.Select(pessoa => new PessoaDto(
            pessoa.Id,
            pessoa.Nome, 
            pessoa.Cpf, 
            pessoa.DataNascimento
        )).ToList();
    }
}