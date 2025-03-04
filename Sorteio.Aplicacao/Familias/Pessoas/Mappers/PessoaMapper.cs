using Sorteio.Aplicacao.Familias.Pessoas.Dtos;
using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Aplicacao.Familias.Pessoas.Mappers;

public static class PessoaMapper
{
    public static PessoaDto ParaDto(Pessoa pessoa) => new PessoaDto
    (
        pessoa.Id,
        pessoa.Nome,
        pessoa.Cpf,
        pessoa.DataNascimento,
        pessoa.Renda
    );
    
    public static ArmazenaPessoaDto ParaArmazenaPessoaDto(Pessoa pessoa) => new ArmazenaPessoaDto
    (
        pessoa.Nome,
        pessoa.Cpf,
        pessoa.DataNascimento,
        pessoa.Renda
    );
}