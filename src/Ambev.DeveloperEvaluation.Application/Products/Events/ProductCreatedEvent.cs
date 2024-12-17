
namespace Ambev.DeveloperEvaluation.Application.Products.Events;

public class ProductCreatedEvent
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
}
