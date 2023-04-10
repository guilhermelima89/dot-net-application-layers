using Api.Extensions;
using Api.ViewModels;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ProdutoController : MainController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IProdutoService _produtoService;

    public ProdutoController(
        INotificadorService notificador,
        IUserService appUser,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IProdutoService ProdutoService
        ) : base(notificador, appUser)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _produtoService = ProdutoService;
    }

    [HttpGet]
    public async Task<IEnumerable<ProdutosViewModel>> ObterTodos([FromQuery] QueryStringParameters queryStringParameters)
    {
        var lista = await _unitOfWork.ProdutoRepository.ObterLista(queryStringParameters);

        var metadata = new PaginacaoViewModel<Produto>(lista);

        Response.Headers.Add("X-Pagination", metadata.ToJson());

        return _mapper.Map<IEnumerable<ProdutosViewModel>>(lista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var item = _mapper.Map<ProdutoViewModel>(await _unitOfWork.ProdutoRepository.ObterDetalhes(id));

        return item is null ? NotFound() : CustomResponse(item);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(ProdutoViewModel Produto)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _produtoService.Adicionar(_mapper.Map<Produto>(Produto));

        return CustomResponse();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ProdutoViewModel Produto)
    {
        if (id != Produto.Id) return BadRequest();

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _produtoService.Atualizar(_mapper.Map<Produto>(Produto));

        return CustomResponse();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var item = await _unitOfWork.ProdutoRepository.GetByIdAsync(id);

        if (item is null) return NotFound();

        await _produtoService.Remover(id);

        return CustomResponse();
    }
}
