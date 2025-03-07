using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Dominio.Familias;

public class Familia
{
    public int Id { get; private set; }
    public int ResponsavelId { get; private set; }
    public virtual Pessoa Responsavel { get; private set; }
    public int? ConjugeId { get; private set; }
    public virtual Pessoa? Conjuge { get; private set; }
    public virtual List<Pessoa> Dependentes { get; private set; } = new List<Pessoa>(); 
    
    protected Familia() { }
    public Familia(Pessoa responsavel)
    {
        Responsavel = responsavel;
    }
    
    public Familia(Pessoa responsavel, List<Pessoa> dependentes)
    {
        Responsavel = responsavel;
        AdicionarDependentes(dependentes);
    }
    
    public void AdicionarConjuge(Pessoa conjuge)
    {
        Conjuge = conjuge;
    }
    
    public void AdicionarDependentes(List<Pessoa> dependentes)
    {
        Dependentes.AddRange(dependentes);
    }

    public void AdicionarDependente(Pessoa dependente)
    {
        Dependentes.Add(dependente);
    }

    private bool ValidarCpfDuplicidadeResponsavel(string cpf)
    {
        return Responsavel.Cpf != cpf;
    }

    private bool ValidarCpfDuplicidadeConjuge(string cpf)
    {
        return Conjuge == null || Conjuge.Cpf != cpf;
    }
    
    private bool ValidarCpfDuplicidadeDependentes(string cpf)
    {
        return Dependentes.Count == 0 || Dependentes.All(dependente => dependente.Cpf != cpf);
    }
    
    public int ObterQuantidadeDependentesComMenoridade()
    {
        return Dependentes.Count(dependente => dependente.ObterIdade() < 18);
    }
    
    public float ObterRendaFamiliar()
    {
        return Responsavel.Renda + (Conjuge != null ? Conjuge.Renda : 0);
    }
}