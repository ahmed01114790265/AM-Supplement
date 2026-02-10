using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Services;
using AM_Supplement.Presentation.Models;
using AM_Supplement.Shared.Enums;
using AM_Supplement.Shared.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Drawing.Printing;

namespace AM_Supplement.Presentation.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public ProductController(IProductService productService, IStringLocalizer<SharedResource> localizer)
        {
            _productService = productService;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetProductsList(1, 6);

            if (!result.IsValid || result.ModelList == null)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction("ProductsList");
            }

            var model = new ProductsListViewModel
            {
                Products = result.ModelList,
                TotalPages = result.TotalPages,
               
            };

            return View("ProductsList", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsList(
     int pageNumber = 1,
     int pageSize = 6,
     ProductType? productTypeFilter = null,
     TypeSorting? sorting = null)
        {
            var result = await _productService.GetProductsList(pageNumber, pageSize, productTypeFilter, sorting);

            if (!result.IsValid || result.ModelList == null)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction("Index");
            }

            var model = new ProductsListViewModel
            {
                Products = result.ModelList,
                TotalPages = result.TotalPages,
                CurrentPage = pageNumber
            };

            return View("ProductsList", model);
        }


        [HttpGet]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var result = await _productService.GetProductById(id);
            if (!result.IsValid)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction("Index");
            }
            return View(result.Model);
        }

    }


}
