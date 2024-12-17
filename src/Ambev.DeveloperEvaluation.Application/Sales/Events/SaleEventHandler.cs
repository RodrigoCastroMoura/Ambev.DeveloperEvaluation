
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Sales.Events;

public class SaleEventHandler : 
    IHandleMessages<SaleCreatedEvent>,
    IHandleMessages<SaleModifiedEvent>,
    IHandleMessages<SaleCancelledEvent>,
    IHandleMessages<ItemCancelledEvent>
{
    private readonly ILogger<SaleEventHandler> _logger;

    public SaleEventHandler(ILogger<SaleEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(SaleCreatedEvent message)
    {
        _logger.LogInformation("Sale created: {Id} for customer {Customer}", 
            message.Sale.Id, message.Sale.Customer);
        await Task.CompletedTask;
    }

    public async Task Handle(SaleModifiedEvent message)
    {
        _logger.LogInformation("Sale modified: {Id}", message.Sale.Id);
        await Task.CompletedTask;
    }

    public async Task Handle(SaleCancelledEvent message)
    {
        _logger.LogInformation("Sale cancelled: {Id}", message.Sale.Id);
        await Task.CompletedTask;
    }

    public async Task Handle(ItemCancelledEvent message)
    {
        _logger.LogInformation("Item cancelled in sale: {SaleId}", message.Item.SaleId);
        await Task.CompletedTask;
    }
}
