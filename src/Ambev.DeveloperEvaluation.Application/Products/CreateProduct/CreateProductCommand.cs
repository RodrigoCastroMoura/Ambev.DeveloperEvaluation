
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public record CreateProductCommand(
    string Title,
    decimal Price,
    string Description,
    string Category,
    string Image,
    decimal Rate,
    int Count
) : IRequest<CreateProductResult>;
