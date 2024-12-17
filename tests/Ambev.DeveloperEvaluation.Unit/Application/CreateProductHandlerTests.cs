
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Moq;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateProductHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IBus> _busMock;

    public CreateProductHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _mapperMock = new Mock<IMapper>();
        _busMock = new Mock<IBus>();
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateProduct()
    {
        // Arrange
        var command = new CreateProductCommand("Test Product", 100m, "Description", "Category", "image.jpg", 4.5m, 10);
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Price = command.Price,
            Description = command.Description,
            Category = command.Category,
            Image = command.Image,
            Rating = new Rating { Rate = command.Rate, Count = command.Count }
        };
        var result = new CreateProductResult { Id = product.Id, Title = command.Title };

        _mapperMock.Setup(m => m.Map<CreateProductResult>(It.IsAny<Product>())).Returns(result);
        _productRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Product>(), default)).ReturnsAsync(product);

        var handler = new CreateProductHandler(_productRepositoryMock.Object, _mapperMock.Object, _busMock.Object);

        // Act
        var response = await handler.Handle(command, default);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(command.Title, response.Title);
        _productRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Product>(), default), Times.Once);
    }
}
