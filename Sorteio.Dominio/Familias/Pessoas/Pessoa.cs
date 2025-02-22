using Sorteio.Dominio.Recursos;

namespace Sorteio.Dominio.Familia.Pessoas;

public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    
    public Pessoa(string nome, string cpf, DateTime dataNascimento)
    {
        cpf = cpf.SomenteNumeros();
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
        ValidarNome(nome);
        ValidarDataNascimento(dataNascimento);
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

}