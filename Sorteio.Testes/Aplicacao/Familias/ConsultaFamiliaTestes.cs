using FluentAssertions;
using Moq;
using Sorteio.Aplicacao.Familias.Consultas;
using Sorteio.Dominio.Familias;
using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Aplicacao.Familias;

public class ConsultaFamiliaTestes
{
    private readonly Mock<IFamiliaRepository> _familiaRepositoryMock;
    private readonly ConsultaFamilia _consultaFamilia;
    
    public ConsultaFamiliaTestes()
    {
        _familiaRepositoryMock = new Mock<IFamiliaRepository>();
        _consultaFamilia = new ConsultaFamilia(_familiaRepositoryMock.Object);
    }
    
    [Fact]
    public async Task DeveObterFamiliasQuandoIdForEncontrado()
    {
        var familia = MontarFamilia();
        var dependente = MontarDependente();
        _familiaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
            .ReturnsAsync(familia);
        _familiaRepositoryMock.Setup(repo => repo.ObterDependentesPorFamiliaIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<Pessoa>(){dependente});
        
        var result = await _consultaFamilia.ObterPorIdAsync(It.IsAny<int>());

        result.Should().NotBeNull();
        result.Id.Should().Be(familia.Id);
        result.Responsavel.Nome.Should().Be(familia.Responsavel.Nome);
        result.Conjuge.Nome.Should().Be(familia.Conjuge.Nome);
        result.RendaFamiliar.Should().Be(familia.ObterRendaFamiliar());
        result.Dependentes.Count.Should().Be(familia.Dependentes.Count());
    }

    [Fact]
    public async Task NaoDeveObterFamiliasQuandoIdNaoForEncontrado()
    {
        _familiaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Familia)null);

        var acao = async () => await _consultaFamilia.ObterPorIdAsync(It.IsAny<int>());

        await acao.Should()
            .ThrowAsync<ArgumentException>()
            .WithMessage(Mensagens.FormatarMensagem(Mensagens.NaoFoiEncontrada, "familia"));
    }

    [Fact]
    public async Task DeveObterFamiliasQuandoObterTodos()
    {
        var familia = MontarFamilia();
        var dependente = MontarDependente();
        
        _familiaRepositoryMock.Setup(repo => repo.ObterTodosAsync())
            .ReturnsAsync([familia]);
        _familiaRepositoryMock.Setup(repo => repo.ObterDependentesPorFamiliaIdAsync(It.IsAny<int>()))
            .ReturnsAsync([dependente]);
        
        var result = await _consultaFamilia.ObterTodosAsync();
        
        result.Should().NotBeEmpty();
        result.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task DeveObterListaVaziaQuandoObterTodosSeNaoExistirFamiliasCadastradas()
    {
        _familiaRepositoryMock.Setup(repo => repo.ObterTodosAsync())
            .ReturnsAsync([]);
        
        var result = await _consultaFamilia.ObterTodosAsync();

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    private Familia MontarFamilia()
    {
        var familia = new Familia(new PessoaBuilder().Novo().ComCpf(AuxiliadorCpf.ObterCpfValido(0)).ComMaioridade().Criar());
        familia.AdicionarConjuge(new PessoaBuilder().Novo().ComCpf(AuxiliadorCpf.ObterCpfValido(1)).ComMaioridade().Criar());
        return familia;        
    }

    private Pessoa MontarDependente()
    {
        return new PessoaBuilder().Novo().ComCpf(AuxiliadorCpf.ObterCpfValido(2)).ComMenoridade().Criar();
    }
}