using Consul;
using InfoQ.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shop.Core.Extensions
{
    public static class Consul
    {
        //public static IServiceCollection AddConsul(this IServiceCollection service, IConfiguration configuration)
        //{
        //    using (var client = new ConsulClient())
        //    {
        //        var tokenOptions = configuration.GetSection("ServiceSettings").Get<BaseOptions>();
        //        var register = new AgentServiceRegistration()
        //        {
        //            ID = tokenOptions.Consul.Id,
        //            Name = tokenOptions.Consul.Name,
        //            Address = tokenOptions.Consul.Address,
        //            Port = tokenOptions.Consul.Port,
        //            Check = new AgentCheckRegistration()
        //            {
        //                HTTP = tokenOptions.Consul.Check.Http,
        //                Interval = TimeSpan.FromSeconds(tokenOptions.Consul.Check.Interval)
        //            }
        //        };
        //        client.Agent.ServiceRegister(register).Wait();
        //    }
        //    return service;
        //}
    }
}