using Bogus;
using Sorteio.Domain.Familia.Pessoas;
using Sorteio.Domain.Familias;
using Sorteio.Familias.Pessoas;

namespace Sorteio.Familias;

public class FamiliaBuilder
{
    private Pessoa _responsavel;
    private List<Pessoa> _dependentes = new List<Pessoa>();

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

    public Familia Montar()
    {
        var familia = new Familia(_responsavel);
        familia.AdicionarDependentes(_dependentes);
        return familia;
    }
}