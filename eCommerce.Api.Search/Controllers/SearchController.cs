using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using eCommerce.Api.Search.Interfaces;
using eCommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchInterface;

        public SearchController(ISearchService searchInterface)
        {
            this.searchInterface = searchInterface;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {

            var customer = await searchInterface.SearchAsync(term.CustomerId);
            if (customer.IsSuccess && customer.SearchResults != null)
            {
                return Ok(customer.SearchResults);
            }
            return NotFound();
        }
    }
}
