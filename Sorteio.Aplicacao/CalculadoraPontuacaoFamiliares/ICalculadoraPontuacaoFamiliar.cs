using Sorteio.Dominio.Familias;

namespace Sorteio.Servico.CalculadoraPontuacaoFamiliares;

public interface ICalculadoraPontuacaoFamiliar
{
    int Calcular(Familia familia);
}