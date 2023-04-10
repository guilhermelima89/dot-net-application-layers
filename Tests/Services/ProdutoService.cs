using Core.Interfaces;
using Core.Models;
using Core.Services;
using Moq;
using Xunit;

namespace Tests.Services;

public class ProdutoServiceTests
{
    private readonly ProdutoService _produtoService;

    public ProdutoServiceTests()
    {
        var repositoryMock = new Mock<IProdutoRepository>();
        var notificador = new Mock<INotificadorService>();
        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(x => x.ProdutoRepository).Returns(repositoryMock.Object);

        _produtoService = new ProdutoService(notificador.Object, unitOfWork.Object);
    }

    private sealed class DataGenerator : TheoryData<string, bool>
    {
        public DataGenerator()
        {
            Add("Informação de teste", true);

            Add("Informação de teste com 51 digitossssssssssssssssss", false);
            Add("", false);
            Add(null, false);
        }
    }

    [Theory(DisplayName = "Adicionar Produto")]
    [ClassData(typeof(DataGenerator))]
    public async void Produto_Adiciona_RetornarSucessoOuFalha(string descricao, bool status)
    {
        // Arrange
        var Produto = new Produto { Descricao = descricao };

        // Act
        var result = await _produtoService.Adicionar(Produto);

        // Assert  
        Assert.Equal(result, status);
    }

    [Theory(DisplayName = "Atualizar Produto")]
    [ClassData(typeof(DataGenerator))]
    public async void Produto_Atualizar_RetornarSucessoOuFalha(string descricao, bool status)
    {
        // Arrange
        var Produto = new Produto { Descricao = descricao };

        // Act
        var result = await _produtoService.Atualizar(Produto);

        // Assert  
        Assert.Equal(result, status);
    }
}
