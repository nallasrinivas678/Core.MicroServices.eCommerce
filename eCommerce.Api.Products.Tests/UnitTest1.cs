using eCommerce.Api.Products.Db;
using eCommerce.Api.Products.Providers;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using eCommerce.Api.Products.Profiles;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace eCommerce.Api.Products.Tests
{
    public class ProuductsServiceTests
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                              .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                              .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productsProvider = new ProductsProvider(dbContext,null,mapper);
            var product = await productsProvider.GetProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i= 1;i<=10;i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal) (i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
