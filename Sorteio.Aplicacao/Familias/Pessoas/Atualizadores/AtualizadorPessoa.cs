using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Aplicacao.Familias.Pessoas.Atualizadores;

public class AtualizadorPessoa : IAtualizadorPessoa
{
    private readonly IPessoaRepository _pessoaRepository;

    public AtualizadorPessoa(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    public async Task AtualizarAsync(int id, AtualizadorPessoaDto atualizadorPessoaDto)
    {
        var pessoa = await ValidarEntradas(id);
        await AtualizarDados(atualizadorPessoaDto, pessoa);
    }

    private async Task<Pessoa> ValidarEntradas(int id)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id);
        if (pessoa == null)
            throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.NaoFoiEncontrada, "Pessoa"));
        
        return pessoa;
    }

    private async Task AtualizarDados(AtualizadorPessoaDto pessoaDto, Pessoa pessoa)
    {
        if(!string.IsNullOrWhiteSpace(pessoaDto.Nome))
            pessoa.AlterarNome(pessoaDto.Nome);
        
        if(!string.IsNullOrWhiteSpace(pessoaDto.Cpf))
            pessoa.AlterarCpf(pessoaDto.Cpf);
        
        if(pessoaDto.DataNascimento != null)
            pessoa.AlterarDataNascimento((DateTime)pessoaDto.DataNascimento);
        
        if(pessoaDto.Renda != null)
            pessoa.AlterarRenda((float) pessoaDto.Renda);
        
        await _pessoaRepository.AtualizarAsync(pessoa);
    }
}