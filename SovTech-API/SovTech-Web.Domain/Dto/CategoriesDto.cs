using System;
using System.Collections.Generic;

namespace SovTech_Web.Domain.Dto
{
    public class CategoriesDto
    {
        public CategoriesDto()
        {
            Categories = new List<string>();
        }

        public List<string> Categories { get; set; }
    }
}
