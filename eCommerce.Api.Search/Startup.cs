using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Search.Interfaces;
using eCommerce.Api.Search.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace eCommerce.Api.Search
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
            services.AddHttpClient("OrdersService", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Orders"]);
            });
            services.AddHttpClient("ProductsService", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Products"]);
            })
            .AddTransientHttpErrorPolicy(p=>p.WaitAndRetryAsync(5,_=>TimeSpan.FromMilliseconds(500)));
            services.AddHttpClient("CustomersService", config =>
            {
                config.BaseAddress = new Uri(Configuration["Services:Customers"]);
            });
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IProductService, ProductsService>();
            services.AddScoped<ICustomersService, CustomersService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //routing
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
