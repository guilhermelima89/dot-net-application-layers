using Core.Interfaces;
using Data.Context;

namespace Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    private IProdutoRepository _produtoRepository;
    public IProdutoRepository ProdutoRepository => _produtoRepository ??= new ProdutoRepository(_context);

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
