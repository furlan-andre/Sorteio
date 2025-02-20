using Sorteio.Domain.Familias;
using Sorteio.Familias;

namespace Sorteio.Servico.CalculadoraPontuacao;

public class CalculadoraPontuacaoDependentesTeste
{
    private readonly CalculadoraPontuacaoDependentes _calculadoraPontuacaoDependentes;
    private readonly string _cpfValido;
    private readonly string[] _cpfDependenteValido = new string[4];
    
    public CalculadoraPontuacaoDependentesTeste()
    {
        _cpfValido = "798.630.670-08";
        _cpfDependenteValido[0] = "031.572.322-07";
        _cpfDependenteValido[1] = "792.972.730-09";
        _cpfDependenteValido[2] = "571.986.750-34";
        _cpfDependenteValido[3] = "914.507.010-51";
        _calculadoraPontuacaoDependentes = new CalculadoraPontuacaoDependentes();
    }
    
    [Theory]
    [InlineData(0, 0)]
    [InlineData(2, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void CalculaPontuacaoDependentes(int resultadoEsperado, int quantidadeDepenentes)
    {
        var familia = ObterFamilia(quantidadeDepenentes);
        var pontuacao = _calculadoraPontuacaoDependentes.Calcular(familia);
        Assert.Equal(resultadoEsperado, pontuacao);
    }

    private Familia ObterFamilia(int quantidadeDepenedentes)
    {
        var familia = new FamiliaBuilder().Novo().ComResponsavel(_cpfValido);

        while (quantidadeDepenedentes > 0)
        {
            familia.ComDependenteMenoridade(_cpfDependenteValido[quantidadeDepenedentes--]);
        }
        
        return familia.Montar();
    }
}


