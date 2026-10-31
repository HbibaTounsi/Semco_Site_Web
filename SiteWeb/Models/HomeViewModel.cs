using SiteWeb.Application.DTOs.Category;
using SiteWeb.Application.DTOs.Product;

namespace SiteWeb.Models
{
    public class HomeViewModel
    {
        public IReadOnlyList<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public IReadOnlyList<ProductListDto> FeaturedProducts { get; set; } = new List<ProductListDto>();
        public IReadOnlyList<ProductListDto> RecentProducts { get; set; } = new List<ProductListDto>();
        public int TotalProductsCount { get; set; }
    }

}
