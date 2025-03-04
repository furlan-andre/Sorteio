using Sorteio.Dominio.Familias.Pessoas;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Dominio.Familias;

public class FamiliaTeste
{   
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DeveCriarFamilia(bool possuiConjuge)
    {
        var responsavel = MontarPessoa(AuxiliadorCpf.ObterCpfValido(0));
        
        var familia = new Familia(responsavel);
        if(possuiConjuge)
        {
            var conjuge = MontarPessoa(AuxiliadorCpf.ObterCpfValido(1));
            familia.AdicionarConjuge(conjuge);
        }
        
        Assert.Equal(responsavel.Nome, familia.Responsavel.Nome);
        Assert.Equal(responsavel.Cpf, familia.Responsavel.Cpf);
        Assert.Equal(responsavel.DataNascimento, familia.Responsavel.DataNascimento);
        Assert.True(!possuiConjuge || !string.IsNullOrWhiteSpace(familia.Conjuge.Cpf));
    }
    
    [Theory]
    [InlineData(1000, 500)]
    [InlineData(1000, 0)]
    public void DeveCriarFamiliaComRendaFamiliar(float rendaResponsavel, float rendaConjuge)
    {
        var rendaFamiliar = rendaResponsavel + rendaConjuge;
        var responsavel = MontarPessoa(AuxiliadorCpf.ObterCpfValido(0), false, rendaResponsavel);
        var conjuge= MontarPessoa(AuxiliadorCpf.ObterCpfValido(1), false, rendaConjuge);
        
        var familia = new Familia(responsavel);
        familia.AdicionarConjuge(conjuge);
        
        Assert.Equal(rendaFamiliar, familia.ObterRendaFamiliar());
    }
    
    [Fact]
    public void DeveCriarFamiliaSemDependentes()
    {
        var responsavel = MontarPessoa(AuxiliadorCpf.ObterCpfValido(0));
        
        var familia = new Familia(responsavel);
        
        Assert.Equal(responsavel.Nome, familia.Responsavel.Nome);
        Assert.Equal(responsavel.Cpf, familia.Responsavel.Cpf);
        Assert.Equal(responsavel.DataNascimento, familia.Responsavel.DataNascimento);
    }
    
        
    [Fact]
    public void DeveCriarFamiliaComUmDependente()
    {
        var responsavel = MontarPessoa(AuxiliadorCpf.ObterCpfValido(0));
        var dependente = MontarPessoa(AuxiliadorCpf.ObterCpfValido(1),true);
        
        var familia = new Familia(responsavel);
        familia.AdicionarDependente(dependente);
        
        Assert.Equal(dependente.Nome, familia.Dependentes.FirstOrDefault().Nome);
        Assert.Equal(dependente.Cpf, familia.Dependentes.FirstOrDefault().Cpf);
        Assert.Equal(dependente.ObterIdade(), familia.Dependentes.FirstOrDefault().ObterIdade());
    }
    
    [Fact]
    public void DeveCriarFamiliaComMaisDeUmDependente()
    {
        var responsavel = MontarPessoa(AuxiliadorCpf.ObterCpfValido(0));
        var dependente1 = MontarPessoa(AuxiliadorCpf.ObterCpfValido(1), true);
        var dependente2 = MontarPessoa(AuxiliadorCpf.ObterCpfValido(2), true);
        
        var familia = new Familia(responsavel);
        familia.AdicionarDependentes([dependente1, dependente2]);
        
        Assert.Contains(familia.Dependentes, pessoa => 
            pessoa.Cpf == dependente1.Cpf && 
            pessoa.ObterIdade() == dependente1.ObterIdade());
        
        Assert.Contains(familia.Dependentes, pessoa => 
            pessoa.Cpf == dependente2.Cpf &&
            pessoa.ObterIdade() == dependente2.ObterIdade());
    }
    
    [Fact]
    public void NaoDeveAdicionarDependenteComCpfJaCadastradoParaResponsavel()
    {
        var responsavel = MontarPessoa(AuxiliadorCpf.ObterCpfValido(0));
        var dependente = MontarPessoa(AuxiliadorCpf.ObterCpfValido(0), true);
        
        var familia = new Familia(responsavel);
        
        var excecao = Assert.Throws<ArgumentException>(() => familia.AdicionarDependente(dependente));
        Assert.Equal(Mensagens.OCpfInformadoJaEstaCadastrado, excecao.Message);
    }
    
    [Fact]
    public void NaoDeveAdicionarDependenteComCpfJaCadastradoParaOutroDependente()
    {
        var responsavel = MontarPessoa(AuxiliadorCpf.ObterCpfValido(0));
        var dependente1 = MontarPessoa(AuxiliadorCpf.ObterCpfValido(1), true);
        var dependente2 = MontarPessoa(AuxiliadorCpf.ObterCpfValido(1), true);
        
        var familia = new Familia(responsavel);
        
        var excecao = Assert.Throws<ArgumentException>(() => familia.AdicionarDependentes([dependente1, dependente2]));
        Assert.Equal(Mensagens.OCpfInformadoJaEstaCadastrado, excecao.Message);
    }

    private Pessoa MontarPessoa(string cpf, bool comMenoridade = false, float renda = 0)
    {
        var builder = new PessoaBuilder()
            .Novo()
            .ComRenda(renda)
            .ComCpf(cpf);
        
         if(comMenoridade)                   
            return builder.ComMenoridade().Criar();
         
         return builder.ComMaioridade().Criar();
    }
}