using SiteWeb.Application.DTOs.Category;
using SiteWeb.Application.DTOs.Product;

namespace SiteWeb.Models
{
    public class ProductListViewModel
    {
        public IReadOnlyList<ProductListDto> Products { get; set; } = new List<ProductListDto>();
        public IReadOnlyList<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public CategoryDto? SelectedCategory { get; set; }
        public string? SearchTerm { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string? SortBy { get; set; }
        public int TotalCount { get; set; }
    }
}
