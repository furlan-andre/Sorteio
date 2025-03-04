using FluentAssertions;
using Moq;
using Sorteio;
using Sorteio.Aplicacao.Familias.Pessoas.Atualizadores;
using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Dominio.Recursos;

public class AtualizadorPessoaTests
{
    private readonly Mock<IPessoaRepository> _pessoaRepositoryMock;
    private readonly AtualizadorPessoa _atualizadorPessoa;

    public AtualizadorPessoaTests()
    {
        _pessoaRepositoryMock = new Mock<IPessoaRepository>();

        _atualizadorPessoa = new AtualizadorPessoa(_pessoaRepositoryMock.Object);
    }

    [Fact]
    public async Task DeveAtualizarPessoaComSucesso()
    {
        var pessoa = new PessoaBuilder().Novo().ComMaioridade().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).Criar();
        var atualizadorPessoaDto = new AtualizadorPessoaDto()
        {
            Nome = "João Das Couves",
            Cpf = AuxiliadorCpf.ObterCpfValido(1),
            DataNascimento = DateTime.Today.AddDays(-365),
            Renda = 100000
        };
        
        _pessoaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(pessoa);
        await _atualizadorPessoa.AtualizarAsync(1, atualizadorPessoaDto);

        pessoa.Nome.Should().Be(atualizadorPessoaDto.Nome);
        pessoa.Cpf.Should().Be(atualizadorPessoaDto.Cpf.SomenteNumeros());
        pessoa.DataNascimento.Should().Be(atualizadorPessoaDto.DataNascimento);
        pessoa.Renda.Should().Be(atualizadorPessoaDto.Renda);

        _pessoaRepositoryMock.Verify(repo => 
            repo.AtualizarAsync(It.Is<Pessoa>(p => 
                p.Nome == atualizadorPessoaDto.Nome &&
                p.Cpf == atualizadorPessoaDto.Cpf.SomenteNumeros() &&
                p.DataNascimento == atualizadorPessoaDto.DataNascimento &&
                p.Renda == atualizadorPessoaDto.Renda)), Times.Once);
    }

    [Fact]
    public async Task DeveLancarExcecaoParaPessoaNaoEncontrada()
    {
        var mensagemEsperada = Mensagens.FormatarMensagem(Mensagens.NaoFoiEncontrada, "Pessoa");
        var pessoaDto = new AtualizadorPessoaDto()
        {
            Nome = "Joao Das Couves"
        };
    
        Func<Task> act = async () => await _atualizadorPessoa.AtualizarAsync(1, pessoaDto);
    
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage(mensagemEsperada);
    
        _pessoaRepositoryMock.Verify(repo => repo.AtualizarAsync(It.IsAny<Pessoa>()), Times.Never);
    }
    
    [Fact]
    public async Task NaoDeveAtualizarCasoValoresInvalidosOuNaoPreenchidos()
    {
        var pessoa = new PessoaBuilder().Novo().ComMaioridade().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).Criar();
        var atualizadorPessoaDto = new AtualizadorPessoaDto() { };
       
        _pessoaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(pessoa);
    
        await _atualizadorPessoa.AtualizarAsync(1, atualizadorPessoaDto);
    
        pessoa.Nome.Should().NotBe(atualizadorPessoaDto.Nome);
        pessoa.Cpf.Should().NotBe(atualizadorPessoaDto.Cpf);
        pessoa.DataNascimento.Should().NotBe(atualizadorPessoaDto.DataNascimento);
        pessoa.Renda.Should().NotBe(atualizadorPessoaDto.Renda);
        
        _pessoaRepositoryMock.Verify(repo => repo.AtualizarAsync(It.IsAny<Pessoa>()), Times.Once);
    }
 }
