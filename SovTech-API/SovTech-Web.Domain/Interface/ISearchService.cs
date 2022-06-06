using SovTech_Web.Domain.Dto;
using System.Threading.Tasks;

namespace SovTech_Web.Domain.Interface
{
    public interface ISearchService
    {
        Task<ResponseDto<SearchDetailDto>> SearchByQuery(string query);
    }
}
