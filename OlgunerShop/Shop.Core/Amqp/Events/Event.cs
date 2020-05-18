using System;

namespace Shop.Core.Amqp.Events
{
    public interface IEvent
    {

    }
    public abstract class Event:IEvent
    {
        public DateTime TimeStamp { get; protected set; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
