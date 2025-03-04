using System.Text.Json.Serialization;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Dominio.Familias.Pessoas;

public class Pessoa
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public float Renda { get; private set; }
    public int? FamiliaId { get; private set; }
    [JsonIgnore] 
    public Familia Familia { get; set; }
    
    protected Pessoa() { }
    
    public Pessoa(string nome, string cpf, DateTime dataNascimento, float renda = 0)
    {
        cpf = cpf.SomenteNumeros();
        ValidarDadosObrigatorios(nome, cpf, dataNascimento, renda);
        
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Renda = renda;
    }

    public int ObterIdade()
    {
        var hoje = DateTime.Today;
        var idade = hoje.Year - DataNascimento.Date.Year;
        
        if (DataNascimento.Date > hoje.AddYears(-idade))
        {
            idade--;
        }
        
        return idade;
    }
    
    private void ValidarDadosObrigatorios(string nome, string cpf, DateTime dataNascimento, float renda)
    {
        ValidarCpf(cpf);
        ValidarNome(nome);
        ValidarDataNascimento(dataNascimento);
        ValidarRenda(renda);
    }

    private void ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf) || !ValidadorDocumento.ValidarCpf(cpf))
            throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.CampoInvalido, "CPF"));
    }
    
    private void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.CampoObrigatorio, "nome"));
    }

    private void ValidarDataNascimento(DateTime dataNascimento)
    {
        if (dataNascimento.Date == new DateTime().Date)
            throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.CampoObrigatorio, "data de nascimento"));
    }

    private void ValidarRenda(float renda = 0)
    {
        if (renda < 0)
            throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.CampoInvalido, "renda"));
    }
    
    public void AlterarNome(string nome)
    {
        ValidarNome(nome);
        Nome = nome;
    }
    
    public void AlterarDataNascimento(DateTime dataNascimento)
    {
        ValidarDataNascimento(dataNascimento);
        DataNascimento = dataNascimento;
    }
    
    public void AlterarCpf(string cpf)
    {
        var cpfSomenteNumeros = cpf.SomenteNumeros();
        ValidarCpf(cpfSomenteNumeros);
        Cpf = cpfSomenteNumeros;
    }

    public void AlterarRenda(float renda)
    {
        ValidarRenda(renda);
        Renda = renda;
    }
}