using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using InfoQ.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shop.Core.Amqp.Bus;
using Shop.Core.Amqp.Infrastructure;
using Shop.Core.CrossCutting.Logging;
using Shop.Core.CrossCutting.Logging.NLog;
using Shop.Core.DataAccess.Http;
using Shop.Core.DataAccess.Mongo;
using Shop.Domain.Amqp.Events;
using Shop.ProductService.Api.Configurations.Log;
using Shop.ProductService.Api.Repositories;
using Shop.ProductService.Business.Abstract;
using Shop.ProductService.Business.Concrete;

namespace Shop.ProductService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<BaseOptions>(Configuration.GetSection("ServiceSettings"));
            var baseOptions = Configuration.GetSection("ServiceSettings").Get<BaseOptions>();
            services.AddSingleton<ILogManager, NLogManager>();
            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));
            // MongoDb
            services.AddMongoDB(Configuration);

            //Log Configurations
            services.AddSingleton<ILogManager, NLogManager>();
            services.AddNLogConfig("/nlog.config");

            //Amqp DI
            //services.AddTransient<IEventBus, RabbitMqBus>();
            services.AddSingleton<IEventBus, RabbitMqBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var logFactory = sp.GetRequiredService<ILogManager>();
                return new RabbitMqBus(sp.GetService<IMediator>(), baseOptions, logFactory, scopeFactory);
            });
            //Generic Repository
            services.AddTransient<IProductServices, ProductManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            ConfigureEventBus(app);
        }


        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //eventBus.Subscribe<ProductChangeEvent, ProductChangeEventHandler>();
        }
    }
}
