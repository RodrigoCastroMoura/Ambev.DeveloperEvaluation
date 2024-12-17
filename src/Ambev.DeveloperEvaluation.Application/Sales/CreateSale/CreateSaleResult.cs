
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleResult
{
    public Guid Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Branch { get; set; } = string.Empty;
}
