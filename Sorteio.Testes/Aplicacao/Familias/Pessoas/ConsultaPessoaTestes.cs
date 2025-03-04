using FluentAssertions;
using Moq;
using Sorteio.Aplicacao.Familias.Pessoas.Consultas;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Aplicacao.Pessoas;

public class ConsultaPessoaTests
{
    private readonly Mock<IPessoaRepository> _pessoaRepositoryMock;
    private readonly ConsultaPessoa _consultaPessoa;
    
    public ConsultaPessoaTests()
    {
        _pessoaRepositoryMock = new Mock<IPessoaRepository>();
        _consultaPessoa = new ConsultaPessoa(_pessoaRepositoryMock.Object);
    }
    
    [Fact]
    public async Task DeteReotornarUmaPessoaValidaQuandoBuscaPorId()
    {
        var pessoa = MontarPessoa();
        _pessoaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(pessoa);
            
        var result = await _consultaPessoa.ObterPorIdAsync(1);
    
        result.Should().NotBeNull();
        result.Nome.Should().Be(pessoa.Nome);
        result.Cpf.Should().Be(pessoa.Cpf);
        result.DataNascimento.Should().Be(pessoa.DataNascimento);
    }
    
    [Fact]
    public async Task DeveRetornarUmaExcecaoParaPessoaNaoEncontradaQuandoBuscaPorId()
    {
        var mensagemEsperada = Mensagens.FormatarMensagem(Mensagens.NaoFoiEncontrada, "pessoa"); 
        await FluentActions
            .Invoking(() => _consultaPessoa.ObterPorIdAsync(1))
            .Should()
            .ThrowAsync<ArgumentException>()
            .WithMessage(mensagemEsperada);
    }
    
    [Fact]
    public async Task DeveRetornarUmaListaDePessoasQuandoObterTodos()
    {
        var pessoa = MontarPessoa();
        var pessoas = new List<Pessoa> { pessoa };
        _pessoaRepositoryMock.Setup(repo => repo.ObterTodosAsync())
            .ReturnsAsync(pessoas);
        
        var result = await _consultaPessoa.ObterTodosAsync();
    
        result.Should().NotBeEmpty();
        result.Should().HaveCount(1);
        result.Should().ContainSingle(p => p.Nome == pessoa.Nome);
        result.Should().ContainSingle(p => p.Cpf == pessoa.Cpf);
    }
    
    [Fact]
    public async Task DeveObterListaVaziaQuandoObterTodosSeNaoExistirPessoasCadastradas()
    {
        _pessoaRepositoryMock.Setup(repo => repo.ObterTodosAsync())
            .ReturnsAsync([]);
        
        var result = await _consultaPessoa.ObterTodosAsync();

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
    
    private Pessoa MontarPessoa()
    {
        var cpfValido = "798.630.670-08";
        return new PessoaBuilder().Novo().ComCpf(cpfValido).ComMaioridade().Criar();
    }
}
