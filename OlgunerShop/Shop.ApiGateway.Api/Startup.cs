using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.IO;
using System.Reflection;

namespace Shop.ApiGateway.Api
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
            services.AddOcelot(Configuration);
            services.AddControllers();

            #region  Swagger entegrasyonu
            services.AddSwaggerGen((options) =>
            {
                options.SwaggerGeneratorOptions.IgnoreObsoleteActions = true;
                options.SwaggerDoc(Configuration.GetSection("Swagger:SwaggerName").Value, new OpenApiInfo()
                {
                    Version = Configuration.GetSection("Swagger:SwaggerDoc:Version").Value,
                    Title = Configuration.GetSection("Swagger:SwaggerDoc:Title").Value,
                    Description = Configuration.GetSection("Swagger:SwaggerDoc:Description").Value,
                    TermsOfService = new Uri(Configuration.GetSection("Swagger:SwaggerDoc:TermsOfService").Value),
                    Contact = new OpenApiContact
                    {
                        Name = Configuration.GetSection("Swagger:SwaggerDoc:Contact:Name").Value,
                        Email = string.Empty,
                        Url = new Uri(Configuration.GetSection("Swagger:SwaggerDoc:Contact:Url").Value),
                    },
                    License = new OpenApiLicense
                    {
                        Name = Configuration.GetSection("Swagger:SwaggerDoc:License:Name").Value,
                        Url = new Uri(Configuration.GetSection("Swagger:SwaggerDoc:License:Url").Value),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            #endregion

         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region added swagger UI

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: String.Format(Configuration.GetSection("Swagger:UseSwaggerUI:SwaggerEndpoint").Value, Configuration.GetSection("Swagger:SwaggerName").Value),
                    name: Configuration.GetSection("Swagger:UseSwaggerUI:Name").Value);
            });

            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot();
        }
    }
}