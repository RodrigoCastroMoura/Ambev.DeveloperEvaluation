
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Sales.Services;

public class SaleService : ISaleService
{
    private const int MaxQuantity = 20;
    private const int MinQuantityForDiscount = 4;
    private const int HighDiscountQuantity = 10;
    private const decimal LowDiscountRate = 0.1m;
    private const decimal HighDiscountRate = 0.2m;

    public decimal CalculateDiscount(int quantity, decimal unitPrice)
    {
        if (quantity < MinQuantityForDiscount) return 0;
        
        var discountRate = quantity >= HighDiscountQuantity ? HighDiscountRate : LowDiscountRate;
        return unitPrice * quantity * discountRate;
    }

    public bool ValidateQuantity(int quantity)
    {
        return quantity > 0 && quantity <= MaxQuantity;
    }
}
