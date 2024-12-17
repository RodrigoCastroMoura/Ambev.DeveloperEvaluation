
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IBus bus)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null) throw new ArgumentException("Sale not found");

        _mapper.Map(request, sale);
        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
        await _bus.Publish(new SaleModifiedEvent(updatedSale));
        
        return _mapper.Map<UpdateSaleResult>(updatedSale);
    }
}
