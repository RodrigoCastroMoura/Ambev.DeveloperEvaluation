using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponseWithData<CreateProductResponse>>> Create([FromBody] CreateProductRequest request)
    {
        var command = _mapper.Map<CreateProductCommand>(request);
        var result = await _mediator.Send(command);
        var response = _mapper.Map<CreateProductResponse>(result);
        var apiResponse = new ApiResponseWithData<CreateProductResponse>(response);
        return new ActionResult<ApiResponseWithData<CreateProductResponse>>(apiResponse);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ApiResponseWithData<GetProductResult>>> Get(Guid id)
    {
        var query = new GetProductCommand(id);
        var result = await _mediator.Send(query);
        return new ApiResponseWithData<GetProductResult>(result);
    }

}
