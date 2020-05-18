using Shop.Core.Amqp.Events;
using Shop.Core.Entities;
using System.Threading.Tasks;

namespace Shop.Core.Amqp.Bus
{
    public interface IEventHandler<in TEvent> : IEvent where TEvent : Event
    {
        Task Handle(TEvent @event);
    }
}