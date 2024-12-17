
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

namespace Ambev.DeveloperEvaluation.Application.Sales;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<SaleItemCommand, SaleItem>();
        CreateMap<Sale, CreateSaleResult>();
        CreateMap<Sale, GetSaleResult>();
        CreateMap<SaleItem, SaleItemResult>();
        CreateMap<UpdateSaleCommand, Sale>();
        CreateMap<Sale, UpdateSaleResult>();
    }
}
