using Api.AutoMapper;
using AutoMapper;
using Xunit;

namespace Tests.Mapper;

public class MapperTests
{
    [Fact]
    public void AutoMapperDtoToModelCliente_Configuration_IsValid()
    {
        var produtoMapper = new MapperConfiguration(cfg => cfg.AddProfile<ProdutoMapper>());
        produtoMapper.AssertConfigurationIsValid();
    }
}
