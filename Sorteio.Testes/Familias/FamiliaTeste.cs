using Sorteio.Domain.Familias;
using Sorteio.Domain.Recursos;
using Sorteio.Familias.Pessoas;

namespace Sorteio.Familias;

public class FamiliaTeste
{   
    private readonly string _cpfValido;
    private readonly string _cpfDependenteValido1;
    private readonly string _cpfDependenteValido2;
    
    public FamiliaTeste()
    {   
        _cpfValido = "798.630.670-08";
        _cpfDependenteValido1 = "031.572.322-07";
        _cpfDependenteValido2 = "792.972.730-09";
    }
    
    [Fact]
    public void DeveCriarFamiliaSemDependentes()
    {
        var responsavel = new PessoaBuilder().Novo().ComCpf(_cpfValido).ComMaioridade().Criar();
        
        var familia = new Familia(responsavel);
        
        Assert.Equal(responsavel.Nome, familia.ObterResponsavel().Nome);
        Assert.Equal(responsavel.Cpf, familia.ObterResponsavel().Cpf);
        Assert.Equal(responsavel.DataNascimento, familia.ObterResponsavel().DataNascimento);
    }
    
        
    [Fact]
    public void DeveCriarFamiliaComUmDependente()
    {
        var responsavel = new PessoaBuilder().Novo().ComCpf(_cpfValido).ComMaioridade().Criar();
        var dependente = new PessoaBuilder().Novo().ComCpf(_cpfDependenteValido1).ComMenoridade().Criar();
        
        var familia = new Familia(responsavel);
        familia.AdicionarDependente(dependente);
        
        Assert.Equal(dependente.Nome, familia.ObterDependentes().FirstOrDefault().Nome);
        Assert.Equal(dependente.Cpf, familia.ObterDependentes().FirstOrDefault().Cpf);
        Assert.Equal(dependente.ObterIdade(), familia.ObterDependentes().FirstOrDefault().ObterIdade());
    }
    
    [Fact]
    public void DeveCriarFamiliaComMaisDeUmDependente()
    {
        var responsavel = new PessoaBuilder().Novo().ComCpf(_cpfValido).ComMaioridade().Criar();
        var dependente1 = new PessoaBuilder().Novo().ComCpf(_cpfDependenteValido1).ComMenoridade().Criar();
        var dependente2 = new PessoaBuilder().Novo().ComCpf(_cpfDependenteValido2).ComMenoridade().Criar();
        
        var familia = new Familia(responsavel);
        familia.AdicionarDependentes([dependente1, dependente2]);
        
        Assert.Contains(familia.ObterDependentes(),
            pessoa => pessoa.Cpf == dependente1.Cpf && pessoa.ObterIdade() == dependente1.ObterIdade());
        
        Assert.Contains(familia.ObterDependentes(),
            pessoa => pessoa.Cpf == dependente2.Cpf && pessoa.ObterIdade() == dependente2.ObterIdade());
    }
    
    [Fact]
    public void NaoDeveAdicionarDependenteComCpfJaCadastrado()
    {
        var responsavel = new PessoaBuilder().Novo().ComCpf(_cpfValido).ComMaioridade().Criar();
        var dependente = new PessoaBuilder().Novo().ComCpf(_cpfValido).ComMenoridade().Criar();
        
        var familia = new Familia(responsavel);
        
        var excecao = Assert.Throws<ArgumentException>(() => familia.AdicionarDependente(dependente));
        Assert.Equal(Mensagens.OCpfInformadoJaEstaCadastrado, excecao.Message);
    }
}