using Bogus;
using Sorteio.Dominio.Familia.Pessoas;
using Sorteio.Dominio.Recursos;

namespace Sorteio.Dominio.Familias.Pessoas;

public class PessoaBuilder
{
    private string _nome;
    private string _cpf;
    private DateTime _dataNascimento;
    private Faker _faker;

    public PessoaBuilder Novo()
    {
        _faker = new Faker();
        _nome = _faker.Name.FullName();
        return this;
    }

    public PessoaBuilder ComCpf(string cpf)
    {
        _cpf = cpf;
        return this;
    }

    public PessoaBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }

    public PessoaBuilder ComMaioridade()
    {
        _dataNascimento = AuxiliadorDatas.ObterDataNascimento(18, 99);
        return this;
    }
    
    public PessoaBuilder ComMenoridade()
    {
        _dataNascimento = AuxiliadorDatas.ObterDataNascimento(0, 17);
        return this;
    }
    
    public Pessoa Criar()
    {
        return new Pessoa(_nome, _cpf, _dataNascimento);
    }
}