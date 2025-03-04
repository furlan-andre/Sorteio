using Sorteio.Dominio.Familias;

namespace Sorteio.Aplicacao.CalculadoraPontuacaoFamiliares.CalculadoraCriterio.Dependentes;

public class CalculadoraCriterioDependentes : ICalculadoraCriterio
{
    public int Calcular(Familia familia)
    {
        var dependentesValidos = familia.ObterQuantidadeDependentesComMenoridade();
        switch (dependentesValidos)
        {
            case >= 3:
                return 3;
            case 2:
            case 1:
                return 2;
            default:
                return 0;
        }
    }
}