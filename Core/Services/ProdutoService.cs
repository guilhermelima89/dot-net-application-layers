using Core.Interfaces;
using Core.Models;
using Core.Validations;

namespace Core.Services;

public class ProdutoService : BaseService, IProdutoService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProdutoService(INotificadorService notificador, IUnitOfWork unitOfWork) : base(notificador)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Adicionar(Produto produto)
    {
        if (!ExecutarValidacao(new ProdutoValidation(), produto)) return false;

        if (_unitOfWork.ProdutoRepository.WhereAsync(x => x.Descricao.ToUpper() == produto.Descricao.ToUpper()).Result.Any())
        {
            Notificar(MessageValidation.INFORMACAO_JA_EXISTE);
            return false;
        }

        produto.Descricao = produto.Descricao.ToUpper();

        _unitOfWork.ProdutoRepository.Add(produto);

        await _unitOfWork.Commit();

        return true;
    }

    public async Task<bool> Atualizar(Produto produto)
    {
        if (!ExecutarValidacao(new ProdutoValidation(), produto)) return false;

        if (_unitOfWork.ProdutoRepository.WhereAsync(x => x.Descricao.ToUpper() == produto.Descricao.ToUpper() && x.Id != produto.Id).Result.Any())
        {
            Notificar(MessageValidation.INFORMACAO_JA_EXISTE);
            return false;
        }

        produto.Descricao = produto.Descricao.ToUpper();

        _unitOfWork.ProdutoRepository.Update(produto);

        await _unitOfWork.Commit();

        return true;
    }

    public async Task<bool> Remover(int id)
    {
        _unitOfWork.ProdutoRepository.Remove(id);

        await _unitOfWork.Commit();

        return true;
    }
}
