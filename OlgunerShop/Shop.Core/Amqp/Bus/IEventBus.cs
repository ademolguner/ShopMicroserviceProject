using Shop.Core.Amqp.Commands;
using Shop.Core.Amqp.Events;
using System.Threading.Tasks;

namespace Shop.Core.Amqp.Bus
{
    public interface IEventBus
    {
        Task SendCommand<T>(T command) where T : Command;

        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>() where T : Event where TH : IEventHandler<T>;
    }
}