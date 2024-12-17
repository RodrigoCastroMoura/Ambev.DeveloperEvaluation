
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequest
{
    [Required]
    public string Title { get; set; }
    [Required]
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Image { get; set; }
    public decimal Rate { get; set; }
    public int Count { get; set; }
}
