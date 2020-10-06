﻿using eCommerce.Api.Search.Interfaces;
using eCommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Providers
{
    public class ProductsService : IProductService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ILogger<IProductService> logger;

        public ProductsService(IHttpClientFactory clientFactory, ILogger<IProductService> logger)
        {
            this.clientFactory = clientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> 
            GetProductsAsync()
        {
            try
            {
                var client = clientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync($"api/products/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }
    }
}
