using AutoMapper;
using eCommerce.Api.Products.Db;
using eCommerce.Api.Products.Interfaces;
using eCommerce.Api.Products.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;


        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 1000});
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 500 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 150, Inventory = 250 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 400, Inventory = 200 });
                dbContext.SaveChanges();
            }
        }
        public Task<(bool IsSuccess, IEnumerable<Models.Product>, string ErrorMessage)> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
