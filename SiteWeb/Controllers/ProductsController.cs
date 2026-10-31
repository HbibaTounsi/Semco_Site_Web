using Microsoft.AspNetCore.Mvc;
using SiteWeb.Application.DTOs.Product;
using SiteWeb.Application.Interfaces;
using SiteWeb.Models;

namespace SiteWeb.Controllers
{
    public class ProductsController : Controller
    {
        
            private readonly IProductService _productService;
            private readonly ICategoryService _categoryService;

            public ProductsController(IProductService productService, ICategoryService categoryService)
            {
                _productService = productService;
                _categoryService = categoryService;
            }

            // GET: /Products
            [Route("Products")]
            [Route("produits")]
            public async Task<IActionResult> Index(string? search, int? categoryId, string? sort)
            {
                var filter = new ProductFilterDto
                {
                    SearchTerm = search,
                    CategoryId = categoryId,
                    SortBy = sort
                };

                var products = await _productService.SearchAsync(filter);
                var categories = await _categoryService.GetAllActiveAsync();

                var viewModel = new ProductListViewModel
                {
                    Products = products,
                    Categories = categories,
                    SearchTerm = search,
                    SelectedCategoryId = categoryId,
                    SortBy = sort,
                    TotalCount = products.Count
                };

                return View(viewModel);
            }

            // GET: /Products/Category/{slug}
            [Route("Products/Category/{slug}")]
            [Route("produits/categorie/{slug}")]
            [Route("Categories/{slug}")]
            public async Task<IActionResult> Category(string slug, string? search, string? sort)
            {
                var category = await _categoryService.GetBySlugAsync(slug);
                if (category == null)
                {
                    return NotFound();
                }

                var filter = new ProductFilterDto
                {
                    SearchTerm = search,
                    CategoryId = category.Id,
                    SortBy = sort
                };

                var products = await _productService.SearchAsync(filter);
                var categories = await _categoryService.GetAllActiveAsync();

                var viewModel = new ProductListViewModel
                {
                    Products = products,
                    Categories = categories,
                    SelectedCategory = category,
                    SelectedCategoryId = category.Id,
                    SearchTerm = search,
                    SortBy = sort,
                    TotalCount = products.Count
                };

                return View(viewModel);
            }

            // GET: /Products/Details/{id} or /produits/{slug}
            [Route("Products/Details/{id:int}")]
            public async Task<IActionResult> DetailsById(int id)
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Details), new { slug = product.Slug });
            }

            [Route("Products/{slug}")]
            [Route("produits/{slug}")]
            public async Task<IActionResult> Details(string slug)
            {
                var product = await _productService.GetBySlugAsync(slug);
                if (product == null)
                {
                    return NotFound();
                }

                var related = await _productService.GetRelatedAsync(product.Id, product.CategoryId, 4);

                var viewModel = new ProductDetailViewModel
                {
                    Product = product,
                    RelatedProducts = related
                };

                return View(viewModel);
            }
        }
    }
