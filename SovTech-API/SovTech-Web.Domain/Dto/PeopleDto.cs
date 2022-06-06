using Newtonsoft.Json;
using System.Collections.Generic;

namespace SovTech_Web.Domain.Dto
{
    public class PeopleDto
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public List<PeopleResultsDto> PeopleResults { get; set; }

        public PeopleDto()
        {
            PeopleResults = new List<PeopleResultsDto>();
        }
    }
}
