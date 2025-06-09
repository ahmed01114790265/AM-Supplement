using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Services;
using AM_Supplement.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AM_Supplement.Presentation.Controllers.ProductController
{
    public class PrroductController : Controller
    {
        IProductService productService;

        public PrroductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpPost]
        public async Task< IActionResult> Index()
        {
           var result = await productService.GetListofProduct(1,6,TypeSortingorFilltring.Featured); 
            if(result.IsVallid==false)
            {
                ViewBag.ErrorMessage = result.ErorrMassege;
                return View();
            }

            return View(result.ModelList);
        }
        [HttpPost]
        public async Task<IActionResult> GetListofProduct(int PageNumber, int PageSize ,TypeSortingorFilltring Sorting)
        {
            var result = await productService.GetListofProduct(PageNumber,PageSize ,Sorting);
            if (result.IsVallid)
            {
                return View(result.ModelList);
            }
           return  RedirectToAction("Index");
            
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
           var result = await productService.DeleteProduct(id);
            if (result.IsVallid)
            {
                TempData["Message"] = "product had deleted";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = result.ErorrMassege;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductDTO productDTO )
        {
            var result = await productService.UpdateProduct(productDTO);
            if (result.IsVallid == false)
            {
                ViewBag.ErrorMessage = result.ErorrMassege;
                return View(productDTO);
            }
            TempData["Message"] = "update is done";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> ReadProduct(Guid id)
        {
            var result = await productService.GetProductById(id);
            if(result.IsVallid)
            {
                return View(result.Model);
            }
            TempData["ErorrMassege"] = result.ErorrMassege;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewProduct(ProductDTO productDTO)
        {
            var result = await productService.AddProduct(productDTO);
            if( result.IsVallid)
            {
                TempData["Massage"] = "New product added";
                return RedirectToAction("Index");
            }
            ViewBag.ErorrMassage = result.ErorrMassege;
            return View(productDTO);
         

        }
    }
   
}
