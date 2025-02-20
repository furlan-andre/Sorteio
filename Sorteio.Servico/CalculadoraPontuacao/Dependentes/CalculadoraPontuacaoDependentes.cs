using Sorteio.Domain.Familias;

namespace Sorteio.Servico.CalculadoraPontuacao.Dependentes;

public class CalculadoraPontuacaoDependentes : ICalculadoraPontuacao
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