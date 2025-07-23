using AM_Supplement.Application.Services;
using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Services;
using AM_Supplement.Shared.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AM_Supplement.Dashboard.Controllers
{
    public class ProductController : Controller
    {
        IProductService ProductService;
        IStringLocalizer<SharedResource> Localizer;

        public ProductController(IProductService productService, IStringLocalizer<SharedResource> localizer)
        {
            ProductService = productService;
            Localizer = localizer;

        }
        public async Task<IActionResult> Index()
        {
            var result = await ProductService.GetProductsList();
            if (result.IsValid == false || result.ModelList == null)
            {
                ViewBag.ErrorMessage = result.ErrorMessage;
                return View("ProductsList", new List<ProductDTO>());
            }

            return View("ProductsList", result.ModelList);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO productDTO)
        {
            if(!ModelState.IsValid)
            {
                return View("AddProduct", productDTO);

            }
            var result = await ProductService.AddProduct(productDTO);
            if (!result.IsValid)
            {
                ModelState.AddModelError("error", result.ErrorMessage);
                return View("AddProduct", productDTO);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAddProductView()
        {
            return View("AddProductForm");
        }
    }
}
