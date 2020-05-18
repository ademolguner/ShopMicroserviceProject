using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shop.Core.Amqp.Bus;
using Shop.Core.Amqp.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoQ.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Shop.Core.Amqp.Commands;
using Shop.Core.CrossCutting.Logging;
using Microsoft.Extensions.Options;

namespace Shop.Core.Amqp.Infrastructure
{
    public sealed class RabbitMqBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly BaseOptions _options;
        private readonly ILogManager _logManager;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RabbitMqBus(IMediator mediator, BaseOptions options,ILogManager logManager, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            _options = options;
            _logManager = logManager;
            _serviceScopeFactory = serviceScopeFactory;

        }
        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Publish<T>(T @event) where T : Event
        {

            var factory = new ConnectionFactory()
            {
                HostName = _options.RabbitMq.HostName,
                UserName = _options.RabbitMq.UserName,
                Password = _options.RabbitMq.Password,
            }; 
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                var eventName = @event.GetType().Name;
                channel.QueueDeclare(eventName, false, false, false, null);
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", eventName, null, body);
            }
        }

        public void Subscribe<T, TH>() where T : Event where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);
            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(p => p.GetType() == handlerType))
            {
                _logManager.Error($"Handler Type {handlerType.Name} already is registered  for '{eventName}'{nameof(handlerType)}");
            }

            _handlers[eventName].Add(handlerType);
            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = _options.RabbitMq.HostName,
                UserName = _options.RabbitMq.UserName,
                Password = _options.RabbitMq.Password,
                DispatchConsumersAsync = true
            }; 
            var eventName = typeof(T).Name;
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(eventName, false, false, false, null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += ConsumerReceivedCallBackAsync;
            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task ConsumerReceivedCallBackAsync(object sender, BasicDeliverEventArgs @event)
        {
            var eventName = @event.RoutingKey;
            var message = Encoding.UTF8.GetString(@event.Body);
            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
                _logManager.Information($"eventName=>{eventName} message=>{message}");
            }
            catch (Exception e)
            {
               _logManager.Error(e.Message);
               _logManager.Error(e.InnerException?.Message);
               _logManager.Error(e.StackTrace);
            }
        }
        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                var subscriptions = _handlers[eventName];
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    foreach (var subscription in subscriptions)
                    {
                        var handler = scope.ServiceProvider.GetService(subscription);
                        if (handler == null) continue;

                        var eventype = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventype);
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventype);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                    }
                }
            }
        }
        //private async Task ProcessEvent(string eventName, string message)
        //{
        //    if (_handlers.ContainsKey(eventName))
        //    {
        //        var subscriptions = _handlers[eventName];
        //        foreach (var subscription in subscriptions)
        //        {
        //            var handler = Activator.CreateInstance(subscription);
        //            if (handler != null)
        //            {
        //                var eventType = _eventTypes.SingleOrDefault(p => p.Name == eventName);
        //                var @event = JsonConvert.DeserializeObject(message, eventType);
        //                var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
        //                await (Task)concreteType.GetMethod("Handle")?.Invoke(handler, new [] { @event });
        //            }

        //        }
        //    }
        //}
    }
}
