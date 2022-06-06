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
    public class ChuckService : IChuckService
    {
        private readonly ExternalApiBaseUrl _apiBaseUrl;
        private readonly ILogger<ChuckService> _logger;

        public ChuckService(ExternalApiBaseUrl apiBaseUrl, ILogger<ChuckService> logger)
        {
            _apiBaseUrl = apiBaseUrl;
            _logger = logger;
        }

        public async Task<ResponseDto<CategoriesDto>> GetJokeCategoriesAsync()
        {
            var categories = new CategoriesDto();
            _logger.LogInformation("Calling chucknorris.io to get all available joke categories");
            var (result, response) = await HttpHelper.GetContentAsync(_apiBaseUrl.ChuckNorrisBaseUrl, "", "categories");

            if(response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<string[]>(result);
                categories.Categories.AddRange(content);

                return ResponseHelper.BuildResponse(true, "Successfully retrieved joke categories data", ResponseHelper.NoErrors, categories);
            }

            return ResponseHelper.BuildResponse(false, "No categories found", ResponseHelper.NoErrors, categories);
        }

        public async Task<ResponseDto<CategoryDetailDto>> GetRandomJokeByCategoryAsync(string category)
        {
            _logger.LogInformation($"Calling chucknorris.io to get random joke for category = {category}");
            var (result, response) = await HttpHelper.GetContentAsync(_apiBaseUrl.ChuckNorrisBaseUrl, "", $"random?category={category}");

            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<CategoryDetailDto>(result);
                return ResponseHelper.BuildResponse(true, $"Successfully retrieved a random joke for category = {category}", 
                    ResponseHelper.NoErrors, content);
            }

            return ResponseHelper.BuildResponse<CategoryDetailDto>(false, $"No random joke found for this category = {category}", 
                ResponseHelper.NoErrors, null); 
        }

        public async Task<ResponseDto<SearchCategoryDto>> SearchCategoryByQueryAsync(string query)
        {
            _logger.LogInformation($"Calling chucknorris.io to search a joke base on query parameter = {query}");
            var (result, response) = await HttpHelper.GetContentAsync(_apiBaseUrl.ChuckNorrisBaseUrl, "", $"search?query={query}");

            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<SearchCategoryDto>(result);
                if(content.Total == 0)
                    return ResponseHelper.BuildResponse<SearchCategoryDto>(false, $"No joke found by querying with = {query}",
                        ResponseHelper.NoErrors, null);

                return ResponseHelper.BuildResponse(true, $"Successfully retrieved a joke by query parameter = {query}",
                    ResponseHelper.NoErrors, content);
            }

            return ResponseHelper.BuildResponse<SearchCategoryDto>(false, $"No joke found by querying with = {query}",
                ResponseHelper.NoErrors, null);
        }
    }
}
