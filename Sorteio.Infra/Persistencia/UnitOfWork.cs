using Microsoft.EntityFrameworkCore.Storage;
using Sorteio.Aplicacao.Persistencia;

namespace Sorteio.Infra;

public class UnitOfWork: IUnitOfWork
{
    private readonly BancoDadosContexto _contexto;
    private IDbContextTransaction? _transacao;

    public UnitOfWork(BancoDadosContexto contexto)
    {
        _contexto = contexto;
    }
    
    public async Task IniciarTransacaoAsync()
    {
        _transacao = await _contexto.Database.BeginTransactionAsync();
    }

    public async Task ConfirmarTransacaoAsync()
    {
        if (_transacao != null)
        {
            await _contexto.SaveChangesAsync();
            await _transacao.CommitAsync();
        }
    }

    public async Task RetrocederTransacaoAsync()
    {
        if (_transacao != null)
        {
            await _transacao.RollbackAsync();
            await _transacao.DisposeAsync();
            _transacao = null;
        }
    }
}