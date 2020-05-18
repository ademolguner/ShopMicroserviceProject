using AutoMapper;
using Shop.BasketService.Api.Entities;
using Shop.Domain.Amqp.Events;

namespace Shop.BasketService.Api.Configurations.Mapping
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