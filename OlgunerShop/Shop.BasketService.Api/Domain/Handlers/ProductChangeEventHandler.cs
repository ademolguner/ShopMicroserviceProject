using Shop.Core.Amqp.Bus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.BasketService.Api.Entities;
using Shop.BasketService.Api.Repositories;
using Shop.Core.Entities;
using Shop.Domain.Amqp.Events;

namespace Shop.BasketService.Api.Domain.Handlers
{
    public class ProductChangeEventHandler : IEventHandler<ProductChangeEvent>
    {
        private IBasketRepository basketRepository;
        private IMapper mapper;
        public ProductChangeEventHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        public async Task Handle(ProductChangeEvent @event)
        {
            var product = mapper.Map<Product>(@event);
            var productInBasket= await basketRepository.GetAsync(product.ProductId);
            productInBasket.UnitPrice = product.UnitPrice;
            productInBasket.UnitsInStock = product.UnitsInStock;
            productInBasket.ProductName = product.ProductName;
            await basketRepository.UpdateAsync(productInBasket);
        }
    }
}
