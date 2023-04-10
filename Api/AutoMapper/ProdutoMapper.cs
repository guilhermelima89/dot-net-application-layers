using Api.ViewModels;
using AutoMapper;
using Core.Models;

namespace Api.AutoMapper;

public class ProdutoMapper : Profile
{
    public ProdutoMapper()
    {
        CreateMap<Produto, ProdutosViewModel>();

        CreateMap<Produto, ProdutoViewModel>();

        CreateMap<ProdutoViewModel, Produto>()
            .ForMember(dest => dest.UsuarioCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.UsuarioAlteracao, opt => opt.Ignore());
    }
}
