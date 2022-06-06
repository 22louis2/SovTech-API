using SovTech_Web.Domain.Dto;
using System.Threading.Tasks;

namespace SovTech_Web.Domain.Interface
{
    public interface ISwapiService
    {
        Task<ResponseDto<PeopleDto>> GetStarWarsPeopleAsync(int pageNumber);
        Task<ResponseDto<PeopleDto>> SearchStarWarsPeopleByQueryAsync(string query);
    }
}
