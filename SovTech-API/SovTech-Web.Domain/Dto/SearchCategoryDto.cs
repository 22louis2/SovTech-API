using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SovTech_Web.Domain.Dto
{
    public class SearchCategoryDto
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("result")]
        public List<CategoryDetailDto> CategoryDetails { get; set; }

        public SearchCategoryDto()
        {
            CategoryDetails = new List<CategoryDetailDto>();
        }
    }
}
