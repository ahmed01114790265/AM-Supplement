using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Services;
using AM_Supplement.Shared.Enums;
using AM_Supplement.Shared.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AM_Supplement.Presentation.Controllers
{
    public class ProductController : Controller
    {
        IProductService productService;
        IStringLocalizer<SharedResource> Localizer;
        public ProductController(IProductService productService, IStringLocalizer<SharedResource> localizer)
        {
            this.productService = productService;
            Localizer = localizer;
        }
        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var result = await productService.GetProductsList();
            if(result.IsValid==false || result.ModelList==null)
            {
                ViewBag.ErrorMessage = result.ErrorMessage;
                return View("ProductsList", new List<ProductDTO>());
            }

            return View("ProductsList", result.ModelList);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsList(int PageNumber, int PageSize , ProductType prodcutTypeFilter,TypeSorting Sorting)
        {
            var result = await productService.GetProductsList(PageNumber,PageSize,prodcutTypeFilter ,Sorting);
            if (result.IsValid)
            {
                return View("Index",result.ModelList);
            }
            TempData["ErrorMessage"] = result.ErrorMessage;
            return  RedirectToAction("Index");
            
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
           var result = await productService.DeleteProduct(id);
            if (result.IsValid)
            {
                TempData["Message"] = "product had deleted";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = result.ErrorMessage;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductDTO productDTO )
        {
            var result = await productService.UpdateProduct(productDTO);
            if (result.IsValid == false)
            {
                ViewBag.ErrorMessage = result.ErrorMessage;
                return View(productDTO);
            }
            TempData["Message"] = "update is done";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var result = await productService.GetProductById(id);
            if(result.IsValid)
            {
                return View(result.Model);
            }
            TempData["ErorrMassege"] = result.ErrorMessage;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
        {
            var result = await productService.AddProduct(productDTO);
            if( result.IsValid)
            {
                TempData["Massage"] = "New product added";
                return RedirectToAction("Index");
            }
            ViewBag.ErorrMassage = result.ErrorMessage;
            return View(productDTO);
         
        }
    }
   
}
