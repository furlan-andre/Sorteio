using Bogus;
using Sorteio.Dominio.Familia.Pessoas;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Dominio.Familias.Pessoas;

public class PessoaTeste
{
    private readonly string _nomeValido;
    private readonly string _cpfValido;
    private readonly DateTime _dataNascimentoValida;

    public PessoaTeste()
    {
        _cpfValido = "798.630.670-08";
        _dataNascimentoValida = DateTime.Now.AddYears(-20);
        var faker = new Faker();
        _nomeValido = faker.Name.FullName();
    }
    
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DeveCriarPessoa(bool comMenoridade)
    {
        var builder = new PessoaBuilder().Novo()
                                         .ComCpf(_cpfValido)
                                         .ComNome(_nomeValido);
        var pessoa = comMenoridade ? 
            builder.ComMenoridade().Criar() : builder.ComMaioridade().Criar();
        
        Assert.Equal(comMenoridade, pessoa.ObterIdade() < 18);
        Assert.Equal(!comMenoridade, pessoa.ObterIdade() >= 18);
        Assert.Equal(_cpfValido.SomenteNumeros(), pessoa.Cpf);
        Assert.Equal(_nomeValido, pessoa.Nome);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)] 
    [InlineData(" ")]
    public void NaoDeveCriarPessoaComNomeInvalido(string nomeInvalido)
    {
        var excecao = Assert.Throws<ArgumentException>(
            () => new Pessoa(nomeInvalido, _cpfValido, _dataNascimentoValida));
        
        Assert.Equal(Mensagens.FormatarMensagem(Mensagens.CampoObrigatorio, "nome"), excecao.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("123")]
    public void NaoDeveCriarPessoaComCpfInvalido(string cpfInvalido)
    {   
        var excecao = Assert.Throws<ArgumentException>(() => new Pessoa(_nomeValido, cpfInvalido, _dataNascimentoValida));
        Assert.Equal(Mensagens.FormatarMensagem(Mensagens.CampoInvalido, "CPF"), excecao.Message);
    }

    [Fact]
    public void NaoDeveCriarPessoaComDataNascimentoInvalida()
    {
        var dataNascimentoInvalida = new DateTime();

        var excecao = Assert.Throws<ArgumentException>(() => new Pessoa(_nomeValido, _cpfValido, dataNascimentoInvalida));
        Assert.Equal(Mensagens.FormatarMensagem(Mensagens.CampoObrigatorio, "data de nascimento"), excecao.Message);
    }
}