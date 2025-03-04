namespace Sorteio.Dominio.Familias.Pessoas;

public interface IPessoaRepository
{
    Task<IEnumerable<Pessoa>> ObterTodosAsync();
    Task<Pessoa> ObterPorIdAsync(int id);
    Task<Pessoa> ObterPorCpf(string cpf);
    Task AdicionarAsync(Pessoa pessoa);
    Task AtualizarAsync(Pessoa pessoa);
}