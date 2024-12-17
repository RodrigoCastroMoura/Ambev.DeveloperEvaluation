
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleService _saleService;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public CreateSaleHandler(ISaleRepository saleRepository, ISaleService saleService, IMapper mapper, IBus bus)
    {
        _saleRepository = saleRepository;
        _saleService = saleService;
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = _mapper.Map<Sale>(command);
        sale.SaleDate = DateTime.SpecifyKind(command.SaleDate, DateTimeKind.Utc);

        foreach (var item in sale.Items)
        {
            item.Discount = _saleService.CalculateDiscount(item.Quantity, item.UnitPrice);
            item.TotalAmount = (item.Quantity * item.UnitPrice) - item.Discount;
        }

        sale.TotalAmount = sale.Items.Sum(i => i.TotalAmount);
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        await _bus.Publish(new SaleCreatedEvent(createdSale));

        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}
