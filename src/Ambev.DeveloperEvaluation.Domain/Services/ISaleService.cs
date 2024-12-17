
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface ISaleService
{
    decimal CalculateDiscount(int quantity, decimal unitPrice);
    bool ValidateQuantity(int quantity);
}
