using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Aplicacao.Familias.Pessoas.Armazenadores;

public class ArmazenadorPessoa : IArmazenadorPessoa
{
    private readonly IPessoaRepository _pessoaRepository;

    public ArmazenadorPessoa(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    public async Task<Pessoa> ArmazenarAsync(ArmazenaPessoaDto pessoaDto)
    {
        var pessoa = new Pessoa(pessoaDto.Nome, pessoaDto.Cpf, pessoaDto.DataNascimento, pessoaDto.Renda);
        var pessoaExistente = await _pessoaRepository.ObterPorCpf(pessoa.Cpf);

        if (pessoaExistente != null)
            throw new ArgumentException(Mensagens.OCpfInformadoJaEstaCadastrado);
        
        await _pessoaRepository.AdicionarAsync(pessoa);
        return pessoa;
    }
}