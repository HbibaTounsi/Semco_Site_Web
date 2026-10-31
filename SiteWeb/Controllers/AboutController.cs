using Microsoft.AspNetCore.Mvc;
using SiteWeb.Application.Interfaces;

namespace SiteWeb.Controllers
{
    public class AboutController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public AboutController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [Route("About")]
        [Route("a-propos")]
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalCategories = (await _categoryService.GetAllActiveAsync()).Count;
            ViewBag.TotalProducts = await _productService.GetTotalCountAsync();
            return View();
        }
    }
}
