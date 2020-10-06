using eCommerce.Api.Search.Interfaces;
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

        public SearchService(IOrdersService ordersService, IProductService productService)
        {
            this.ordersService = ordersService;
            this.productService = productService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)>
            SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productResult = await productService.GetProductsAsync();
            foreach (var order in ordersResult.Orders)
            {
                foreach (var item in order.Items)
                {
                    item.ProductName = productResult.products.
                                                     FirstOrDefault(p => p.Id == item.ProductId)?.Name;
                }
            }
            if (ordersResult.IsSuccess)
            {
                var result = new
                {
                    Orders = ordersResult.Orders,
                    Products = productResult.products
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
