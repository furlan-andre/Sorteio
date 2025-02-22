using Sorteio.Dominio.Familias;

namespace Sorteio.Aplicacao.CalculadoraPontuacaoFamiliares.CalculadoraCriterio;

public interface ICalculadoraCriterio
{
    int Calcular(Familia familia);
}