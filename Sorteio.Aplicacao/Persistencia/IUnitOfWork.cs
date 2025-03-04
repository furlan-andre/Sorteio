namespace Sorteio.Aplicacao.Persistencia;

public interface IUnitOfWork
{
    Task IniciarTransacaoAsync();
    Task ConfirmarTransacaoAsync();
    Task RetrocederTransacaoAsync();
}