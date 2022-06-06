using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SovTech_Web.Domain.Commons;
using SovTech_Web.Domain.Dto;
using SovTech_Web.Domain.Interface;
using SovTech_Web.Domain.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SovTech_Web.Domain.Service
{
    public class SwapiService : ISwapiService
    {
        private readonly ExternalApiBaseUrl _apiBaseUrl;
        private readonly ILogger<SwapiService> _logger;

        public SwapiService(ExternalApiBaseUrl apiBaseUrl, ILogger<SwapiService> logger)
        {
            _apiBaseUrl = apiBaseUrl;
            _logger = logger;
        }

        public async Task<ResponseDto<PeopleDto>> GetStarWarsPeopleAsync(int pageNumber)
        {
            _logger.LogInformation($"Calling swapi.dev to get all available star wars people by page number {pageNumber}");
            var (result, response) = await HttpHelper.GetContentAsync(_apiBaseUrl.SwapiBaseUrl, "", $"?page={pageNumber}");

            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<PeopleDto>(result);
                return ResponseHelper.BuildResponse(true, $"Successfully retrieved star wars people by page number {pageNumber}", ResponseHelper.NoErrors, content);
            }

            return ResponseHelper.BuildResponse<PeopleDto>(false, $"No stars war people found for page number {pageNumber}", ResponseHelper.NoErrors, null); ;
        }

        public async Task<ResponseDto<PeopleDto>> SearchStarWarsPeopleByQueryAsync(string query)
        {
            _logger.LogInformation($"Calling swapi.dev to search a star wars people base on query parameter = {query}");
            var (result, response) = await HttpHelper.GetContentAsync(_apiBaseUrl.SwapiBaseUrl, "", $"?search={query}");

            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<PeopleDto>(result);
                if (content.Count == 0)
                    return ResponseHelper.BuildResponse<PeopleDto>(false, $"No star wars people found by querying with = {query}",
                        ResponseHelper.NoErrors, null);

                return ResponseHelper.BuildResponse(true, $"Successfully retrieved star wars people base on query parameter = {query}",
                    ResponseHelper.NoErrors, content);
            }

            return ResponseHelper.BuildResponse<PeopleDto>(false, $"No star wars people found by querying with = {query}",
                ResponseHelper.NoErrors, null);
        }
    }
}
