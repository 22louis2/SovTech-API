using SovTech_Web.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SovTech_Web.Domain.Interface
{
    public interface IChuckService
    {
        Task<ResponseDto<CategoriesDto>> GetJokeCategoriesAsync();
        Task<ResponseDto<CategoryDetailDto>> GetRandomJokeByCategoryAsync(string category);
        Task<ResponseDto<SearchCategoryDto>> SearchCategoryByQueryAsync(string query);
    }
}
