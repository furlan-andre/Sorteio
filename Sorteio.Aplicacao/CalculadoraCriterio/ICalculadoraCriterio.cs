using Sorteio.Domain.Familias;

namespace Sorteio.Servico.CalculadoraPontuacao;

public interface ICalculadoraCriterio
{
    int Calcular(Familia familia);
}