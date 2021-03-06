using AutoMapper;
using InfoQ.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceStack;
using Shop.BasketService.Api.Configurations.Infrastructure;
using Shop.BasketService.Api.Configurations.Log;
using Shop.BasketService.Api.Domain.Handlers;
using Shop.BasketService.Business.Abstract;
using Shop.BasketService.Business.Concrete;
using Shop.Core.Amqp.Bus;
using Shop.Core.Amqp.Infrastructure;
using Shop.Core.CrossCutting.Logging;
using Shop.Core.CrossCutting.Logging.NLog;
using Shop.Core.DataAccess.Mongo;
using Shop.Domain.Amqp.Events;

namespace Shop.BasketService.Api
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
            services.AddSingleton<IEventBus, RabbitMqBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var logFactory = sp.GetRequiredService<ILogManager>();
                return new RabbitMqBus(sp.GetService<IMediator>(), baseOptions, logFactory, scopeFactory);
            });

            //Subscriptions
            services.AddTransient<ProductChangeEventHandler>();
            services.AddTransient<IEventHandler<ProductChangeEvent>, ProductChangeEventHandler>();

            //Generic Repository
            services.AddTransient<IBasketServices, BasketManager>();

            //services.AddTransient<IHttpRepository<Basket>, HttpRepositoryBase<Basket, HttpClient>>();
            services.AddSingleton<IHttpClient, CustomHttpClient>();
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
            var eventBus = app.ApplicationServices.GetService<IEventBus>();
            eventBus.Subscribe<ProductChangeEvent, ProductChangeEventHandler>();
        }
    }
}