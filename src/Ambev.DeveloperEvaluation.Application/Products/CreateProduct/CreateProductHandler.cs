using Ambev.DeveloperEvaluation.Application.Products.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public CreateProductHandler(IProductRepository productRepository, IMapper mapper, IBus bus)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Title = request.Title,
            Price = request.Price,
            Description = request.Description,
            Category = request.Category,
            Image = request.Image,
            Rating = new Rating
            {
                Rate = request.Rate,
                Count = request.Count
            }
        };

        var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
        var result = _mapper.Map<CreateProductResult>(createdProduct);
    
        await _bus.Publish(new ProductCreatedEvent 
        { 
            Id = createdProduct.Id,
            Title = createdProduct.Title,
            Price = createdProduct.Price,
            Category = createdProduct.Category
        });
    
        return result;
    }
}