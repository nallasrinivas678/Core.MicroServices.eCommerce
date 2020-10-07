using eCommerce.Api.Search.Interfaces;
using eCommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace eCommerce.Api.Search.Providers
{
    public class CustomersService : ICustomersService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ILogger<CustomersService> logger;

        public CustomersService(IHttpClientFactory clientFactory, ILogger<CustomersService> logger)
        {
            this.clientFactory = clientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> 
            GetCustomerAsync(int id)
        {
            try
            {
                var client = clientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<dynamic>(content, options);
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
