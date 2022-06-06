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
    [Produces("application/json")]
    public class ChuckController : ControllerBase
    {
        private readonly IChuckService _chuckService;
        private readonly IServiceProvider _serviceProvider;

        public ChuckController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _chuckService = _serviceProvider.GetRequiredService<IChuckService>();
        }

        /// <summary>
        /// Get jokes categories from chucknorris.io/jokes/categories
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns successful status if categories are found</response>
        /// <response code="404">Returns not found if the categories is empty</response>
        [ProducesResponseType(typeof(ResponseDto<CategoriesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<CategoriesDto>), StatusCodes.Status404NotFound)]
        [HttpGet("categories")]
        public async Task<IActionResult> GetJokeCategories()
        {
            var result = await _chuckService.GetJokeCategoriesAsync();
            if (!result.Status) return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Get random joke by category from chucknorris.io/jokes/random?category={category}
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <response code="200">Returns successful status if random joke is found by a category query</response>
        /// <response code="404">Returns not found if no random joke is found by a category query</response>
        [ProducesResponseType(typeof(ResponseDto<CategoryDetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<CategoryDetailDto>), StatusCodes.Status404NotFound)]
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomJokeByCategory([FromQuery] string category)
        {
            var result = await _chuckService.GetRandomJokeByCategoryAsync(category);
            if (!result.Status) return NotFound(result);

            return Ok(result);
        }
    }
}
