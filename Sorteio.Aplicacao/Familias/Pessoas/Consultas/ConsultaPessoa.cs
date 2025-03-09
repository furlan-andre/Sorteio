using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Aplicacao.Familias.Pessoas.Consultas;

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
        if (pessoa == null) throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.NaoFoiEncontrada,"pessoa")); 
        
        return new PessoaDto(){
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Cpf = pessoa.Cpf,
            DataNascimento = pessoa.DataNascimento,
            Renda = pessoa.Renda,
            FamiliaId = pessoa.FamiliaId
        };
    }

    public async Task<IEnumerable<PessoaDto>> ObterTodosAsync()
    {
        var pessoas = await _pessoaRepository.ObterTodosAsync();
        return pessoas.Select(pessoa => new PessoaDto()
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Cpf = pessoa.Cpf,
            DataNascimento = pessoa.DataNascimento,
            Renda = pessoa.Renda,
            FamiliaId = pessoa.FamiliaId
        }).ToList();
    }
}