using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SovTech_Web.Domain.Dto;
using SovTech_Web.Domain.Interface;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SovTech_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SwapiController : ControllerBase
    {
        private readonly ISwapiService _swapiService;
        private readonly IServiceProvider _serviceProvider;

        public SwapiController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _swapiService = _serviceProvider.GetRequiredService<ISwapiService>();
        }

        /// <summary>
        /// Get star wars people from swapi.dev/api/people?pageNumber={pageNumber}
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        /// <response code="200">Returns successful status if star wars people are found by page number</response>
        /// <response code="404">Returns not found if the star wars people is empty by page number</response>
        [ProducesResponseType(typeof(ResponseDto<PeopleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<PeopleDto>), StatusCodes.Status404NotFound)]
        [HttpGet("people")]
        public async Task<IActionResult> GetStarWarsPeople([FromQuery] int pageNumber)
        {
            var result = await _swapiService.GetStarWarsPeopleAsync(pageNumber);
            if (!result.Status) return NotFound(result);

            return Ok(result);
        }
    }
}
