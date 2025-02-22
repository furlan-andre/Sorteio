using Sorteio.Dominio.Familias;
using Sorteio.Servico.CalculadoraCriterio.Dependentes;

namespace Sorteio.Aplicacao.CalculadoraPontuacao.Dependente;

public class CalculadoraPontuacaoDependentesTeste
{
    private readonly CalculadoraCriterioDependentes _calculadoraCriterioDependentes;
    private readonly string _cpfValido;
    private readonly string[] _cpfDependenteValido = new string[4];
    
    public CalculadoraPontuacaoDependentesTeste()
    {
        _cpfValido = "798.630.670-08";
        _cpfDependenteValido[0] = "031.572.322-07";
        _cpfDependenteValido[1] = "792.972.730-09";
        _cpfDependenteValido[2] = "571.986.750-34";
        _cpfDependenteValido[3] = "914.507.010-51";
        _calculadoraCriterioDependentes = new CalculadoraCriterioDependentes();
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
        var pontuacao = _calculadoraCriterioDependentes.Calcular(familia);
        Assert.Equal(resultadoEsperado, pontuacao);
    }
    
    protected Familia ObterFamilia(int quantidadeDepenedentes)
    {
        var familia = new FamiliaBuilder().Novo().ComResponsavel(_cpfValido);

        while (quantidadeDepenedentes > 0)
        {
            familia.ComDependenteMenoridade(_cpfDependenteValido[--quantidadeDepenedentes]);
        }
        
        return familia.Montar();
    }
}


