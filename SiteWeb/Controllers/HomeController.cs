using Microsoft.AspNetCore.Mvc;
using SiteWeb.Application.Interfaces;
using SiteWeb.Models;
using System.Diagnostics;

namespace SiteWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public HomeController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllActiveAsync();
            var featuredProducts = await _productService.GetFeaturedAsync(6);
            var allProducts = await _productService.GetAllAsync(activeOnly: true);

            var viewModel = new HomeViewModel
            {
                Categories = categories,
                FeaturedProducts = featuredProducts,
                RecentProducts = allProducts.Take(8).ToList(),
                TotalProductsCount = allProducts.Count
            };

            return View(viewModel);
        }
    }
}
