using Sorteio.Dominio.Familias;

namespace Sorteio.Aplicacao.CalculadoraPontuacaoFamiliares;

public interface ICalculadoraPontuacaoFamiliar
{
    int Calcular(Familia familia);
}