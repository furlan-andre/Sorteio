using FluentAssertions;
using Moq;
using Sorteio;
using Sorteio.Aplicacao.CalculadoraPontuacaoFamiliares;
using Sorteio.Aplicacao.Classificacoes;
using Sorteio.Dominio.Familias;

public class ClassificacaoFamiliaTests
{
    private readonly Mock<IFamiliaRepository> _familiaRepositoryMock;
    private readonly Mock<ICalculadoraPontuacaoFamiliar> _calculadoraPontuacaoFamiliarMock;
    private readonly ClassificacaoFamilia _classificacaoFamilia;

    public ClassificacaoFamiliaTests()
    {
        _familiaRepositoryMock = new Mock<IFamiliaRepository>();
        _calculadoraPontuacaoFamiliarMock = new Mock<ICalculadoraPontuacaoFamiliar>();

        _classificacaoFamilia = new ClassificacaoFamilia(
            _familiaRepositoryMock.Object,
            _calculadoraPontuacaoFamiliarMock.Object
        );
    }

    [Fact]
    public async Task DeveRealizarClassificacaoFamiliasDeAcordoComAPontuacao()
    {
        var familias = new List<Familia>
        {
            new FamiliaBuilder().Novo().ComResponsavel(AuxiliadorCpf.ObterCpfValido(0)).Criar(),
            new FamiliaBuilder().Novo().ComResponsavel(AuxiliadorCpf.ObterCpfValido(1)).Criar(),
            new FamiliaBuilder().Novo().ComResponsavel(AuxiliadorCpf.ObterCpfValido(2)).Criar(),
            new FamiliaBuilder().Novo().ComResponsavel(AuxiliadorCpf.ObterCpfValido(3)).Criar()
        };
        _familiaRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(familias);
        int indice = 1;
        foreach (var familia in familias)
        {
            _calculadoraPontuacaoFamiliarMock
                .Setup(calc => calc.Calcular(familia))
                .Returns(indice++);
        }
    
        var resultado = await _classificacaoFamilia.RealizarClassificacao();
    
        resultado.Should().NotBeNullOrEmpty();
        resultado.Should().HaveCount(4);
        resultado[0].CpfResponsavel.Should().Be(familias[3].Responsavel.Cpf);
        resultado[1].CpfResponsavel.Should().Be(familias[2].Responsavel.Cpf);
        resultado[2].CpfResponsavel.Should().Be(familias[1].Responsavel.Cpf);
        resultado[3].CpfResponsavel.Should().Be(familias[0].Responsavel.Cpf);
    }
    
    [Fact]
    public async Task DeveRealizarClassificacaoComDesempatePorDataNascimentoResponsavel()
    {
        var familias = new List<Familia>
        {
            new FamiliaBuilder().Novo().ComResponsavel(AuxiliadorCpf.ObterCpfValido(0)).Criar(),
            new FamiliaBuilder().Novo().ComResponsavel(AuxiliadorCpf.ObterCpfValido(1)).Criar(),
        };
        _familiaRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(familias);
        _calculadoraPontuacaoFamiliarMock.Setup(calculadora => calculadora.Calcular(It.IsAny<Familia>())).Returns(1);
    
        var resultado = await _classificacaoFamilia.RealizarClassificacao();
    
        resultado.First().Pontuacao.Should().Be(resultado.Last().Pontuacao);
        resultado.First().DataNascimentoResponsavel.Should().BeBefore(resultado.Last().DataNascimentoResponsavel);
    }
  
    [Fact]
    public async Task DeveRetornarListaVaziaQuandoNaoHouverFamiliasParaRealizarClassificacao()
    {
        _familiaRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(new List<Familia>());
    
        var resultado = await _classificacaoFamilia.RealizarClassificacao();
    
        resultado.Should().BeEmpty();
    }
}