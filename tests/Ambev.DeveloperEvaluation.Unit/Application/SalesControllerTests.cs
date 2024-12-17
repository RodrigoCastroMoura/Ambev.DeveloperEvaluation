
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class SalesControllerTests
{
    private readonly IMediator _mediator;
    private readonly SalesController _controller;

    public SalesControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _controller = new SalesController(_mediator);
    }

    [Fact]
    public async Task Get_ShouldReturnOk_WhenSaleExists()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        var getSaleResult = new GetSaleResult
        {
            Id = sale.Id,
            Number = sale.Number,
            Customer = sale.Customer
        };
        _mediator.Send(Arg.Any<GetSaleQuery>()).Returns(getSaleResult);

        // Act
        var result = await _controller.Get(sale.Id);

        // Assert
        result.Value.Should().BeOfType<ApiResponseWithData<GetSaleResult>>();
        await _mediator.Received(1).Send(Arg.Any<GetSaleQuery>());
    }

    [Fact]
    public async Task Cancel_ShouldReturnOk_WhenSaleExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mediator.Send(Arg.Any<CancelSaleCommand>()).Returns(true);

        // Act
        var result = await _controller.Cancel(id);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        await _mediator.Received(1).Send(Arg.Any<CancelSaleCommand>());
    }

    [Fact]
    public async Task Delete_ShouldReturnOk_WhenSaleExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mediator.Send(Arg.Any<DeleteSaleCommand>()).Returns(Task.FromResult(true));

        // Act
        var result = await _controller.Delete(id);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        await _mediator.Received(1).Send(Arg.Any<DeleteSaleCommand>());
    }
}
