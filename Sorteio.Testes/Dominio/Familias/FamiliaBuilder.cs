using Bogus;
using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Dominio.Familias;

public class FamiliaBuilder
{
    private Pessoa _responsavel;
    private Pessoa _conjuge;
    private List<Pessoa> _dependentes = new List<Pessoa>();
    private float _renda;

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

    public FamiliaBuilder ComConjuge(string cpf)
    {
        _conjuge = new PessoaBuilder().Novo().ComCpf(cpf).ComMaioridade().Criar();
        return this;
    }
    
    public FamiliaBuilder ComDependenteMenoridade(string cpf)
    {
        var dependente = new PessoaBuilder().Novo().ComCpf(cpf).ComMenoridade().Criar(); 
        _dependentes.Add(dependente);
        return this;
    }

    public FamiliaBuilder ComRenda(float renda)
    {
        _renda = renda;
        return this;
    }
    
    public Familia Criar()
    {
        _responsavel.AlterarRenda(_renda);
        var familia = new Familia(_responsavel);
        familia.AdicionarDependentes(_dependentes);
        if(_conjuge != null) familia.AdicionarConjuge(_conjuge);
        
        return familia;
    }
}