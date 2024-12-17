
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class ProductTests
{
    [Fact(DisplayName = "Given valid product data When creating product Then should create successfully")]
    public void CreateProduct_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Assert
        product.Should().NotBeNull();
        product.Id.Should().NotBeEmpty();
        product.Title.Should().NotBeNullOrEmpty();
        product.Description.Should().NotBeNullOrEmpty();
        product.Price.Should().BeGreaterThan(0);
        product.Category.Should().NotBeNullOrEmpty();
        product.Image.Should().NotBeNullOrEmpty();
        product.Rating.Rate.Should().BeInRange(0, 5);
        product.Rating.Count.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact(DisplayName = "Given product generator When generating multiple products Then should create correct number")]
    public void GenerateProducts_WithCount_ShouldCreateCorrectNumber()
    {
        // Arrange
        const int expectedCount = 5;

        // Act
        var products = ProductTestData.GenerateValidProducts(expectedCount);

        // Assert
        products.Should().HaveCount(expectedCount);
        products.Should().AllBeOfType<Product>();
    }
}
