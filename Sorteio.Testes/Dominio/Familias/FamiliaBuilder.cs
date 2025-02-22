using Bogus;
using Sorteio.Dominio.Familia.Pessoas;
using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Dominio.Familias;

public class FamiliaBuilder
{
    private Pessoa _responsavel;
    private List<Pessoa> _dependentes = new List<Pessoa>();
    private float _rendaFamiliar;

    public FamiliaBuilder Novo()
    {
        new Faker();
        return this;
    }

    public FamiliaBuilder ComResponsavel(string cpf)
    {
        _responsavel = new PessoaBuilder().Novo().ComCpf(cpf).ComMaioridade().Criar();
        return this;
    }

    public FamiliaBuilder ComDependenteMenoridade(string cpf)
    {
        var dependente = new PessoaBuilder().Novo().ComCpf(cpf).ComMenoridade().Criar(); 
        _dependentes.Add(dependente);
        return this;
    }

    public FamiliaBuilder ComRendaFamiliar(float rendaFamiliar)
    {
        _rendaFamiliar = rendaFamiliar;
        return this;
    }
    
    public Familia Montar()
    {
        var familia = new Familia(_responsavel, _rendaFamiliar);
        familia.AdicionarDependentes(_dependentes);
        return familia;
    }
}