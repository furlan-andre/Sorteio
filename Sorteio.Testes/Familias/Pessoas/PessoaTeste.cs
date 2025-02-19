using Sorteio.Domain.Familia.Pessoa;
using Sorteio.Domain.Recursos;

namespace Sorteio.Testes;

public class PessoaTeste
{
    private readonly string _nomeValido;
    private readonly string _cpfValido;
    private readonly DateTime _dataNascimentoValida;
    private readonly Random _random;
    public PessoaTeste()
    {
        _nomeValido = "TESTE";
        _cpfValido = "798.630.670-08";
        _dataNascimentoValida = new DateTime(2000, 01, 05);
        _random = new Random(DateTime.Now.Ticks.GetHashCode());
    }
    
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void DeveCriarPessoa(bool comMenoridade)
    {
        var dataNascimentoValida = new DateTime(1990, 07, 23);
        
        if (comMenoridade)
        {
            var idade = _random.Next(1, 17);
            dataNascimentoValida =  DateTime.Now.AddYears(-idade);
        }
        
        var pessoa = new Pessoa(_nomeValido, _cpfValido, dataNascimentoValida);

        Assert.Equal(comMenoridade, pessoa.ObterIdade() < 18);
        Assert.Equal(!comMenoridade, pessoa.ObterIdade() >= 18);
        Assert.Equal(_nomeValido, pessoa.Nome);
        Assert.Equal(_cpfValido, pessoa.Cpf);
        Assert.Equal(dataNascimentoValida, pessoa.DataNascimento);
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
    [InlineData("" )]
    [InlineData(null)]
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