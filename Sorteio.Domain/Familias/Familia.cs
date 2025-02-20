using Sorteio.Domain.Familia.Pessoas;
using Sorteio.Domain.Recursos;

namespace Sorteio.Domain.Familias;

public class Familia
{
    private Pessoa Responsavel { get; set; }
    private List<Pessoa> Dependentes { get; set; } = new List<Pessoa>();

    public Familia(Pessoa responsavel)
    {
        Responsavel = responsavel;
    }

    public void AdicionarDependentes(List<Pessoa> dependentes)
    {
        dependentes.ForEach(dependente => AdicionarDependente(dependente));
    }

    public void AdicionarDependente(Pessoa dependente)
    {
        ValidarCpfJaCadastrado(dependente);
        
        Dependentes.Add(dependente);
    }

    private void ValidarCpfJaCadastrado(Pessoa dependente)
    {
        if (Responsavel.Cpf == dependente.Cpf || Dependentes.Any(pessoa => pessoa.Cpf == dependente.Cpf))
            throw new ArgumentException(Mensagens.OCpfInformadoJaEstaCadastrado);
    }

    public Pessoa ObterResponsavel()
    {
        return Responsavel;
    }
    
    public IEnumerable<Pessoa> ObterDependentes()
    {
        return Dependentes.Count == 0 ? [] : Dependentes;
    }
}