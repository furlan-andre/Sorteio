using Sorteio.Dominio.Familias;

namespace Sorteio.Aplicacao.CalculadoraPontuacaoFamiliares.CalculadoraCriterio.Dependentes;

public class CalculadoraCriterioDependentes : ICalculadoraCriterio
{
    public int Calcular(Familia familia)
    {
        var dependentes = familia.ObterDependentes().ToList();
        var dependentesComMenoridade = dependentes.Count(dependente => dependente.ObterIdade() < 18);

        switch (dependentesComMenoridade)
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