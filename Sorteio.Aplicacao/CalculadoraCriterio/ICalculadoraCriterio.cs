using Sorteio.Dominio.Familias;

namespace Sorteio.Aplicacao.CalculadoraCriterio;

public interface ICalculadoraCriterio
{
    int Calcular(Familia familia);
}