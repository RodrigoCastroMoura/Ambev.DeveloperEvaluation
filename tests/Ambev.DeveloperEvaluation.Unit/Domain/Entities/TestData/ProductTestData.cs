
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class ProductTestData
{
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Id, f => Guid.NewGuid())
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(p => p.Image, f => f.Image.PicsumUrl());
        

    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    public static List<Product> GenerateValidProducts(int count = 3)
    {
        return ProductFaker.Generate(count);
    }
}
