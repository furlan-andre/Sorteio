using Sorteio.Domain.Familias;

namespace Sorteio.Servico.CalculadoraPontuacao;

public interface ICalculadoraPontuacao
{
    int Calcular(Familia familia);
}