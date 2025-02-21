using Sorteio.Domain.Familias;

namespace Sorteio.Servico.CalculadoraPontuacaoFamiliares;

public interface ICalculadoraPontuacaoFamiliar
{
    int Calcular(Familia familia);
}