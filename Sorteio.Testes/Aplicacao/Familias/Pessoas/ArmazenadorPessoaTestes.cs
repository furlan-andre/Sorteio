using FluentAssertions;
using Moq;
using Sorteio.Aplicacao.Familias.Pessoas.Armazenadores;
using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Aplicacao.Familias.Pessoas.Mappers;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Aplicacao.Pessoas;

public class ArmazenadorPessoaTestes
{
    private readonly Mock<IPessoaRepository> _pessoaRepositoryMock;
    private readonly ArmazenadorPessoa _armazenadorPessoa;
    
    public ArmazenadorPessoaTestes()
    {
        _pessoaRepositoryMock = new Mock<IPessoaRepository>();
        _armazenadorPessoa = new ArmazenadorPessoa(_pessoaRepositoryMock.Object);
    }

    [Fact]
    public async Task DeveArmazenarPessoaNoRepositorio()
    {
        var pessoa = new PessoaBuilder().Novo().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).ComMaioridade().Criar();
        
        await _armazenadorPessoa.ArmazenarAsync(PessoaMapper.ParaArmazenaPessoaDto(pessoa));

        _pessoaRepositoryMock.Verify(repo => repo.AdicionarAsync(It.Is<Pessoa>(
            p => p.Nome == pessoa.Nome &&
                 p.Cpf == pessoa.Cpf &&
                 p.DataNascimento == pessoa.DataNascimento
        )), Times.Once);
    }
    
    [Fact]
    public async Task NaoDeveArmazenarPessoaComCpfJaCadastradoNoRepositorio()
    {
        var pessoa = new PessoaBuilder().Novo().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).ComMaioridade().Criar();
        var mensagemEsperada = Mensagens.OCpfInformadoJaEstaCadastrado;
        _pessoaRepositoryMock.Setup(repo => repo.ObterPorCpf(It.IsAny<string>())).ReturnsAsync(pessoa);
        
        var acao = async () => await _armazenadorPessoa.ArmazenarAsync(PessoaMapper.ParaArmazenaPessoaDto(pessoa));

        await acao.Should()
            .ThrowAsync<ArgumentException>()
            .WithMessage(mensagemEsperada);
        
    }

    [Fact]
    public async Task NaoDeveArmazenarPessoaNoRepositorioQuandoInvalido()
    {
        var pessoa = new PessoaBuilder().Novo().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).ComMaioridade().Criar();
        var mensagemEsperada = Mensagens.FormatarMensagem(Mensagens.CampoInvalido, "CPF");
        
        var acao = async () => await _armazenadorPessoa.ArmazenarAsync(
            new ArmazenaPessoaDto(pessoa.Nome, "", pessoa.DataNascimento));
        
        _pessoaRepositoryMock.Verify(repo => repo.AdicionarAsync(It.Is<Pessoa>(
            p => p.Nome == pessoa.Nome &&
                 p.DataNascimento == pessoa.DataNascimento
        )), Times.Never);
    }    
}