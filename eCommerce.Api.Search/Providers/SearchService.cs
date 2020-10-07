using eCommerce.Api.Search.Interfaces;
using eCommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Providers
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductService productService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService,
                             IProductService productService,
                             ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productService = productService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)>
            SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productResult = await productService.GetProductsAsync();
            var customerResult = await customersService.GetCustomerAsync(customerId);
            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess ?
                                           productResult.products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                                           "Product information is not available";
                    }
                }
                var result = new
                {
                    Orders = ordersResult.Orders,
                    Customer = customerResult.IsSuccess ?
                            customerResult.Customer :
                            new { Name = "Customer Information not available" }
                            //Products = productResult.products
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
