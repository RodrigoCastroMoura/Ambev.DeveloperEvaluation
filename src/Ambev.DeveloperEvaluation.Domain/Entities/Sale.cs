
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public string Number { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Branch { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
    public List<SaleItem> Items { get; set; } = new();
}

public class SaleItem
{
    private const int MaxQuantity = 20;
    
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    private int quantity;
    public int Quantity 
    { 
        get => quantity;
        set
        {
            if (value > MaxQuantity)
                throw new ArgumentException($"Quantity cannot exceed {MaxQuantity} items");
            quantity = value;
        }
    }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
}
