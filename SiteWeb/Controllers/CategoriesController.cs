using Microsoft.AspNetCore.Mvc;
using SiteWeb.Application.Interfaces;

namespace SiteWeb.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: /Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllActiveAsync();
            return View(categories);
        }
    }
}
