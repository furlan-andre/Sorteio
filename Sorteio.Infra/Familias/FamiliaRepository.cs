using Microsoft.EntityFrameworkCore;
using Sorteio.Dominio.Familias;
using Sorteio.Dominio.Familias.Pessoas;

namespace Sorteio.Infra.Familias;

public class FamiliaRepository : IFamiliaRepository
{
    private readonly BancoDadosContexto _contexto;
    private IFamiliaRepository _familiaRepositoryImplementation;

    public FamiliaRepository(BancoDadosContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<Familia>> ObterTodosAsync()
    {
        return await _contexto.Familias
                .AsNoTracking()
                .Include(f => f.Responsavel)
                .Include(f => f.Conjuge)
                .ToListAsync();
    }

    public async Task<Familia> ObterPorIdAsync(int id)
    {
        return await _contexto.Familias
            .Where(f => f.Id == id)
            .Include(f => f.Responsavel)
            .Include(f => f.Conjuge)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Pessoa>> ObterDependentesPorFamiliaIdAsync(int id)
    {
        return await _contexto.Pessoas.Where(pessoa => pessoa.FamiliaId == id).ToListAsync();
    }
    
    public async Task AdicionarAsync(Familia familia)
    {
        _contexto.Familias.Add(familia);
        await _contexto.SaveChangesAsync();
    }
}