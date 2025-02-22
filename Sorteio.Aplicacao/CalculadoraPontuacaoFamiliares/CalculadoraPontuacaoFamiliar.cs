using Sorteio.Dominio.Familias;
using Sorteio.Servico.CalculadoraCriterio.Dependentes;
using Sorteio.Servico.CalculadoraCriterio.RendaFamiliar;
using Sorteio.Servico.CalculadoraPontuacao;

namespace Sorteio.Servico.CalculadoraPontuacaoFamiliares;

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