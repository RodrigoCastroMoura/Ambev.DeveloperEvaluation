
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class ProductsControllerTests
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
        _controller = new ProductsController(_mediator, _mapper);
    }

    [Fact]
    public async Task Get_ShouldReturnOk_WhenProductExists()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        _mediator.Send(Arg.Any<GetProductCommand>()).Returns(new GetProductResult { Id = product.Id, Title = product.Title });

        // Act
        var result = await _controller.Get(product.Id);

        // Assert
        result.Value.Should().BeOfType<ApiResponseWithData<GetProductResult>>();
        await _mediator.Received(1).Send(Arg.Any<GetProductCommand>());
    }

    [Fact]
    public async Task Create_ShouldReturnCreated_WhenValidRequest()
    {
        // Arrange
        var request = new CreateProductRequest 
        { 
            Title = "Test Product",
            Price = 100,
            Description = "Test Description",
            Category = "Test Category",
            Image = "test.jpg",
            Rate = 4.5m,
            Count = 10
        };
        var command = new CreateProductCommand(request.Title, request.Price, request.Description, request.Category, request.Image, request.Rate, request.Count);
        _mapper.Map<CreateProductCommand>(request).Returns(command);
        _mediator.Send(command).Returns(new CreateProductResult());

        // Act
        var result = await _controller.Create(request);

        // Assert
        result.Value.Should().BeOfType<ApiResponseWithData<CreateProductResponse>>();
        await _mediator.Received(1).Send(Arg.Any<CreateProductCommand>());
    }
}
