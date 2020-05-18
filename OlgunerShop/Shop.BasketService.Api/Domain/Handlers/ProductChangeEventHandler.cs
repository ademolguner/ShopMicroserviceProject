using Shop.Core.Amqp.Bus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.BasketService.Api.Entities; 
using Shop.Core.Entities;
using Shop.Domain.Amqp.Events;
using Shop.BasketService.Business.Abstract;

namespace Shop.BasketService.Api.Domain.Handlers
{
    public class ProductChangeEventHandler : IEventHandler<ProductChangeEvent>
    {
        private readonly IBasketServices _basketServices;
        private readonly IMapper mapper;
        public ProductChangeEventHandler(IBasketServices basketServices, IMapper mapper)
        {
            this._basketServices = basketServices;
            this.mapper = mapper;
        }
        public async Task Handle(ProductChangeEvent @event)
        {
            var product = mapper.Map<Product>(@event);
            var productInBasket= await _basketServices.GetAsync(product.ProductId);
            productInBasket.UnitPrice = product.UnitPrice;
            productInBasket.UnitsInStock = product.UnitsInStock;
            productInBasket.ProductName = product.ProductName;
            await _basketServices.UpdateAsync(productInBasket);
        }
    }
}
