
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCreatedEvent
{
    public Sale Sale { get; }
    public SaleCreatedEvent(Sale sale) => Sale = sale;
}

public class SaleModifiedEvent
{
    public Sale Sale { get; }
    public SaleModifiedEvent(Sale sale) => Sale = sale;
}

public class SaleCancelledEvent
{
    public Sale Sale { get; }
    public SaleCancelledEvent(Sale sale) => Sale = sale;
}

public class ItemCancelledEvent
{
    public SaleItem Item { get; }
    public ItemCancelledEvent(SaleItem item) => Item = item;
}
