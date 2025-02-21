using Sorteio.Domain.Familias;
using Sorteio.Servico.CalculadoraPontuacao;

namespace Sorteio.Servico.CalculadoraCriterio.RendaFamiliar;

public class CalculadoraCriterioRendaFamiliar : ICalculadoraCriterio
{
    public int Calcular(Familia familia)
    {
        var rendaFamiliar = familia.ObterRendaFamiliar();
        switch (rendaFamiliar)
        {
            case <= 1500:
                return 4;
            case <= 1700:
                return 3;
            case <= 1800:
                return 2;
            case <= 2000:
                return 1;
            default:
                return 0;
        }
    }
}