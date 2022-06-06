using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SovTech_Web.Domain.Dto;
using SovTech_Web.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SovTech_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly IServiceProvider _serviceProvider;

        public SearchController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _searchService = _serviceProvider.GetRequiredService<ISearchService>();
        }

        /// <summary>
        /// Search both chucknorris.io/jokes/search?query={query} and swapi.dev/api/people?search={query}
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="200">Returns successful status data is returned for the searched query parameter</response>
        /// <response code="404">Returns not found if no data is returned for the searched query</response>
        [ProducesResponseType(typeof(ResponseDto<SearchDetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<SearchDetailDto>), StatusCodes.Status404NotFound)]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var result = await _searchService.SearchByQuery(query);
            if(result.Status) return Ok(result);

            return NotFound(result);
        }
    }
}
