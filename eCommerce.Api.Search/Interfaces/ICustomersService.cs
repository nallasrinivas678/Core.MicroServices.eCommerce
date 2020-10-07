using eCommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)>
           GetCustomerAsync(int Id);
    }
}
