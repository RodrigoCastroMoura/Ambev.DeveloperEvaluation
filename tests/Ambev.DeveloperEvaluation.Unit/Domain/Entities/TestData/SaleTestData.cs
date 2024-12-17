
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(si => si.Id, f => Guid.NewGuid())
        .RuleFor(si => si.ProductId, f => Guid.NewGuid())
        .RuleFor(si => si.Product, f => ProductTestData.GenerateValidProduct())
        .RuleFor(si => si.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(si => si.UnitPrice, f => decimal.Parse(f.Commerce.Price()))
        .RuleFor(si => si.Discount, f => f.Random.Decimal(0, 50))
        .RuleFor(si => si.IsCancelled, f => f.Random.Bool())
        .FinishWith((f, si) => si.TotalAmount = (si.Quantity * si.UnitPrice) - si.Discount);

    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, f => Guid.NewGuid())
        .RuleFor(s => s.Number, f => f.Random.Replace("SALE-####"))
        .RuleFor(s => s.SaleDate, f => f.Date.Recent())
        .RuleFor(s => s.Customer, f => f.Name.FullName())
        .RuleFor(s => s.Branch, f => f.Company.CompanyName())
        .RuleFor(s => s.Items, f => SaleItemFaker.Generate(f.Random.Int(1, 5)))
        .RuleFor(s => s.IsCancelled, f => f.Random.Bool())
        .FinishWith((f, s) => s.TotalAmount = s.Items.Sum(i => i.TotalAmount));

    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    public static List<Sale> GenerateValidSales(int count = 3)
    {
        return SaleFaker.Generate(count);
    }
}
