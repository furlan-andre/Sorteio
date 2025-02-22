using Sorteio.Aplicacao.CalculadoraPontuacaoFamiliares.CalculadoraCriterio;
using Sorteio.Aplicacao.CalculadoraPontuacaoFamiliares.CalculadoraCriterio.Dependentes;
using Sorteio.Aplicacao.CalculadoraPontuacaoFamiliares.CalculadoraCriterio.RendaFamiliar;
using Sorteio.Dominio.Familias;

namespace Sorteio.Aplicacao.CalculadoraPontuacaoFamiliares;

public class CalculadoraPontuacaoFamiliar : ICalculadoraPontuacaoFamiliar
{
    private readonly List<ICalculadoraCriterio> _criterios;

    public CalculadoraPontuacaoFamiliar()
    {
        _criterios = new List<ICalculadoraCriterio>()
        {
            new CalculadoraCriterioDependentes(),
            new CalculadoraCriterioRendaFamiliar()
        };
    }
    
    public int Calcular(Familia familia)
    {
        return _criterios.Sum(criterio => criterio.Calcular(familia));        
    }
}