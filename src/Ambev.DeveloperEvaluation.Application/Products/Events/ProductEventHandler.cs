
using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Products.Events;

public class ProductEventHandler : IHandleMessages<ProductCreatedEvent>
{
    private readonly ILogger<ProductEventHandler> _logger;

    public ProductEventHandler(ILogger<ProductEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ProductCreatedEvent message)
    {
        _logger.LogInformation("Product created: {Title} with price {Price}", message.Title, message.Price);
        await Task.CompletedTask;
    }
}
