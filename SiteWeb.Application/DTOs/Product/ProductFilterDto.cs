using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteWeb.Application.DTOs.Product
{
    public class ProductFilterDto
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public string? CategorySlug { get; set; }
        public string? SortBy { get; set; } // "name_asc", "name_desc", "date_desc"
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }
}
