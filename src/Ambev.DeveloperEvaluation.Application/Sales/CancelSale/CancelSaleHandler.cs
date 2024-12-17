
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBus _bus;

    public CancelSaleHandler(ISaleRepository saleRepository, IBus bus)
    {
        _saleRepository = saleRepository;
        _bus = bus;
    }

    public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null) return false;

        sale.IsCancelled = true;
        foreach (var item in sale.Items)
        {
            item.IsCancelled = true;
            await _bus.Publish(new ItemCancelledEvent(item));
        }

        await _saleRepository.UpdateAsync(sale, cancellationToken);
        await _bus.Publish(new SaleCancelledEvent(sale));
        
        return true;
    }
}
