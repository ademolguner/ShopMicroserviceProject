using Shop.Core.Amqp.Events;
using System;

namespace Shop.Core.Amqp.Commands
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; protected set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}