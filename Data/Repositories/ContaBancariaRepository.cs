using Core.Interfaces;
using Core.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Produto> ObterDetalhes(int id)
    {
        return await Context.Produto
            .Select(x => new Produto
            {
                Id = x.Id,
                Descricao = x.Descricao,
                Ativo = x.Ativo,
                UsuarioCriacao = x.UsuarioCriacao,
                DataCriacao = x.DataCriacao,
                UsuarioAlteracao = x.UsuarioAlteracao,
                DataAlteracao = x.DataAlteracao
            })
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PagedList<Produto>> ObterLista(QueryStringParameters queryStringParameters)
    {
        var lista = Context.Produto
            .OrderBy(x => x.Descricao)
            .Select(x => new Produto
            {
                Id = x.Id,
                Descricao = x.Descricao,
                Ativo = x.Ativo
            })
            .Where(x => EF.Functions.Like(x.Descricao, $"%{queryStringParameters.Query}%"))
            .AsNoTrackingWithIdentityResolution();

        return await PagedList<Produto>.ToPagedList(lista, queryStringParameters.PageNumber, queryStringParameters.PageSize);
    }
}
