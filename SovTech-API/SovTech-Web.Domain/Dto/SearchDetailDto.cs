using System;
using System.Collections.Generic;
using System.Text;

namespace SovTech_Web.Domain.Dto
{
    public class SearchDetailDto
    {
        public SearchCategoryDto CategorySearch { get; set; }
        public PeopleDto PeopleSearch { get; set; }
        public string UrlSearchResultAPI { get; set; }
    }
}
