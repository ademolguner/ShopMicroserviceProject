using AutoMapper;
using Shop.Domain.Amqp.Events;
using Shop.ProductService.Entities.Models;

namespace Shop.ProductService.Api.Configurations.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductChangeEvent>();
            CreateMap<ProductChangeEvent, Product>();
        }
    }
}