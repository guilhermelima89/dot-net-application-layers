using Core.Models;

namespace Core.Interfaces;

public interface IProdutoService
{
    Task<bool> Adicionar(Produto produto);
    Task<bool> Atualizar(Produto produto);
    Task<bool> Remover(int id);
}
