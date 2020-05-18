using MediatR;
using Shop.Core.Amqp.Bus;
using Shop.Domain.Amqp.Events;
using Shop.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.ProductService.Api.Domain.Handlers
{
    public class ProductChangeCommandHandler : IRequestHandler<ProductChangeCommand, bool>
    {
        private readonly IEventBus _eventBus;

        public ProductChangeCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(ProductChangeCommand request, CancellationToken cancellationToken)
        {
            //After Update from  Product Database
            _eventBus.Publish(new ProductChangeEvent(
                request.Id,
                request.ProductId,
                request.ProductName,
                request.CategoryId,
                request.UnitPrice,
                request.UnitsInStock,
                request.PictureUrl
            ));
            //publish event to rabbitMQ
            return Task.FromResult(true);
        }
    }
}