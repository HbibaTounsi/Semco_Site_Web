using SiteWeb.Application.DTOs.Product;

namespace SiteWeb.Models
{
    public class ProductDetailViewModel
    {
        public ProductDto Product { get; set; } = null!;
        public IReadOnlyList<ProductListDto> RelatedProducts { get; set; } = new List<ProductListDto>();
    }
}
