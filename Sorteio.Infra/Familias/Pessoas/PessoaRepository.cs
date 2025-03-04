using Microsoft.EntityFrameworkCore;
using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Infra.Familias.Pessoas;

public class PessoaRepository : IPessoaRepository
{
    private readonly BancoDadosContexto _contexto;

    public PessoaRepository(BancoDadosContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<Pessoa>> ObterTodosAsync()
    {
        return await _contexto.Pessoas.AsNoTracking().ToListAsync();
    }

    public async Task<Pessoa> ObterPorIdAsync(int id)
    {
        return await _contexto.Pessoas.AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Pessoa> ObterPorCpf(string cpf)
    {
        return await _contexto.Pessoas.Where(pessoa => pessoa.Cpf == cpf).FirstOrDefaultAsync();
    }

    public async Task AdicionarAsync(Pessoa pessoa)
    {
        _contexto.Pessoas.Add(pessoa);
        await _contexto.SaveChangesAsync();
    }
    
    public async Task AtualizarAsync(Pessoa pessoa)
    {
        _contexto.Pessoas.Update(pessoa);
        await _contexto.SaveChangesAsync();
    }
}