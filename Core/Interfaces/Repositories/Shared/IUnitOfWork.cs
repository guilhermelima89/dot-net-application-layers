namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task Commit();

    IProdutoRepository ProdutoRepository { get; }
}
