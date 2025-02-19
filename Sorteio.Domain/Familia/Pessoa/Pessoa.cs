using Sorteio.Domain.Recursos;

namespace Sorteio.Domain.Familia.Pessoa;

public class Pessoa
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    
    public Pessoa(string nome, string cpf, DateTime dataNascimento)
    {
        ValidarDadosObrigatorios(nome, cpf, dataNascimento);
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
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
    
    private void ValidarDadosObrigatorios(string nome, string cpf, DateTime dataNascimento)
    {
        ValidarCpf(cpf);
        
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.CampoObrigatorio, "nome"));
        
        if (dataNascimento.Date == new DateTime().Date)
            throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.CampoObrigatorio, "data de nascimento"));
    }

    private void ValidarCpf(string cpf)
    {
        if (!ValidadorDocumento.ValidarCpf(cpf))
            throw new ArgumentException(Mensagens.FormatarMensagem(Mensagens.CampoInvalido, "CPF"));
    }
}