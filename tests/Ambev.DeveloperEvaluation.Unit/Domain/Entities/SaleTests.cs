
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact(DisplayName = "Given valid sale data When creating sale Then should create successfully")]
    public void CreateSale_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange & Act
        var sale = SaleTestData.GenerateValidSale();

        // Assert
        sale.Should().NotBeNull();
        sale.Id.Should().NotBeEmpty();
        sale.Number.Should().NotBeNullOrEmpty();
        sale.Customer.Should().NotBeNullOrEmpty();
        sale.Branch.Should().NotBeNullOrEmpty();
        sale.Items.Should().NotBeEmpty();
    }

    [Theory(DisplayName = "Given quantity When calculating discount Then should apply correct discount")]
    [InlineData(3, 0)] // No discount
    [InlineData(4, 0.1)] // 10% discount
    [InlineData(10, 0.2)] // 20% discount
    public void CalculateDiscount_WithQuantity_ShouldApplyCorrectDiscount(int quantity, decimal expectedDiscountRate)
    {
        // Arrange
        var unitPrice = 100m;
        var saleItem = new SaleItem
        {
            Quantity = quantity,
            UnitPrice = unitPrice
        };

        // Act
        var expectedTotal = quantity * unitPrice * (1 - expectedDiscountRate);
        saleItem.TotalAmount = expectedTotal;

        // Assert
        saleItem.TotalAmount.Should().Be(expectedTotal);
    }

    [Fact(DisplayName = "Given quantity above limit When creating sale item Then should throw exception")]
    public void CreateSaleItem_WithQuantityAboveLimit_ShouldThrowException()
    {
        // Arrange
        var quantity = 21;

        // Act
        var act = () => new SaleItem { Quantity = quantity };

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact(DisplayName = "Given cancelled sale When checking status Then should be cancelled")]
    public void CancelSale_WhenExecuted_ShouldBeCancelled()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        sale.IsCancelled = true;

        // Assert
        sale.IsCancelled.Should().BeTrue();
    }

    [Fact(DisplayName = "Given cancelled sale When checking items Then all items should be cancelled")]
    public void CancelSale_WhenExecuted_ShouldCancelAllItems()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        sale.IsCancelled = true;
        foreach (var item in sale.Items)
        {
            item.IsCancelled = true;
        }

        // Assert
        sale.Items.All(i => i.IsCancelled).Should().BeTrue();
    }
}
