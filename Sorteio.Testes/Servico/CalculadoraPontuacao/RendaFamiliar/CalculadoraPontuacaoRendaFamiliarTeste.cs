using Sorteio.Familias;
using Sorteio.Servico.CalculadoraCriterio.RendaFamiliar;

namespace Sorteio.Servico.CalculadoraPontuacao.RendaFamiliar;

public class CalculadoraPontuacaoRendaFamiliarTeste
{
    private readonly CalculadoraCriterioRendaFamiliar _calculadoraCriterioRendaFamiliar;
    private readonly string _cpfValido;
    
    public CalculadoraPontuacaoRendaFamiliarTeste()
    {
        _cpfValido = "798.630.670-08";
        _calculadoraCriterioRendaFamiliar = new CalculadoraCriterioRendaFamiliar();
    }
    
    [Theory]
    [InlineData(4, 1499)]
    [InlineData(4, 1500)]
    [InlineData(3, 1501)]
    [InlineData(3, 1700)]
    [InlineData(2, 1701)]
    [InlineData(2, 1800)]
    [InlineData(1, 1801)]
    [InlineData(1, 2000)]
    [InlineData(0, 2001)]
    public void CalculaPontuacaoDependentes(int resultadoEsperado, float rendaFamiliar)
    {
        var familia = new FamiliaBuilder().Novo().ComResponsavel(_cpfValido).ComRendaFamiliar(rendaFamiliar).Montar();
        var pontuacao = _calculadoraCriterioRendaFamiliar.Calcular(familia);
        Assert.Equal(resultadoEsperado, pontuacao);
    }
}
