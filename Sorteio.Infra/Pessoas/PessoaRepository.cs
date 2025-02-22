using Microsoft.EntityFrameworkCore;
using Sorteio.Dominio.Familia.Pessoas;
using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Infra.Pessoas;

public class PessoaRepository : IPessoaRepository
{
    private readonly BancoDadosContexto _context;

    public PessoaRepository(BancoDadosContexto context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pessoa>> ObterTodosAsync()
    {
        return await _context.Pessoas.ToListAsync();
    }

    public async Task<Pessoa> ObterPorIdAsync(int id)
    {
        return await _context.Pessoas.Where(p => p.Id == id).FirstOrDefaultAsync();
    }
}