using Sorteio.Aplicacao.Pessoas.Dtos;
using Sorteio.Dominio.Familia.Pessoas;
using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Aplicacao.Pessoas.Armazenadores;

public class ArmazenadorPessoa : IArmazenadorPessoa
{
    private readonly IPessoaRepository _pessoaRepository;

    public ArmazenadorPessoa(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    public async Task ArmazenarAsync(PessoaDto pessoaDto)
    {
        var pessoa = new Pessoa(pessoaDto.Nome, pessoaDto.Cpf, pessoaDto.DataNascimento);
        await _pessoaRepository.AdicionarAsync(pessoa);
    }
}