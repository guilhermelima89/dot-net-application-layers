using Core.Models;

namespace Core.Interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<PagedList<Produto>> ObterLista(QueryStringParameters queryStringParameters);
    Task<Produto> ObterDetalhes(int id);
}
