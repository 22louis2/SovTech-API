using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SovTech_Web.Domain.Commons;
using SovTech_Web.Domain.Dto;
using SovTech_Web.Domain.Interface;
using SovTech_Web.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SovTech_Web.Domain.Service
{
    public class SearchService : ISearchService
    {
        private readonly ExternalApiBaseUrl _apiBaseUrl;
        private readonly IChuckService _chuckService;
        private readonly ISwapiService _swapiService;
        private readonly ILogger<SearchService> _logger;

        public SearchService(ExternalApiBaseUrl apiBaseUrl, IChuckService chuckService, ISwapiService swapiService, ILogger<SearchService> logger)
        {
            _apiBaseUrl = apiBaseUrl;
            _chuckService = chuckService;
            _swapiService = swapiService;
            _logger = logger;
        }

        public async Task<ResponseDto<SearchDetailDto>> SearchByQuery(string query)
        {
            var searchDetailsToReturn = new SearchDetailDto();
            _logger.LogInformation($"Now running search from {_apiBaseUrl.ChuckNorrisBaseUrl}");
            var searchCategoryResult = await _chuckService.SearchCategoryByQueryAsync(query);
            if (searchCategoryResult.Status)
            {
                searchDetailsToReturn.CategorySearch = searchCategoryResult.Data;
                searchDetailsToReturn.UrlSearchResultAPI = _apiBaseUrl.ChuckNorrisBaseUrl;
            }

            _logger.LogInformation($"No running search from {_apiBaseUrl.SwapiBaseUrl}");
            var searchPeopleResult = await _swapiService.SearchStarWarsPeopleByQueryAsync(query);
            if (searchPeopleResult.Status)
            {
                searchDetailsToReturn.PeopleSearch = searchPeopleResult.Data;
                searchDetailsToReturn.UrlSearchResultAPI = _apiBaseUrl.SwapiBaseUrl;
            }

            if (!searchCategoryResult.Status && !searchPeopleResult.Status)
                return ResponseHelper.BuildResponse<SearchDetailDto>(false, $"No search result to be returned for query = {query}",
                    ResponseHelper.NoErrors, null);

            return ResponseHelper.BuildResponse(true, $"Successfully retrieved search result with query parameter = {query}",
                ResponseHelper.NoErrors, searchDetailsToReturn);
        }
    }
}
