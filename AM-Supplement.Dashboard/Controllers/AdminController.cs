using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Services;
using AM_Supplement.Dashboard.Models;
using AM_Supplement.Shared.Enums;
using AM_Supplement.Shared.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AM_Supplement.Dashboard.Controllers
{

    //private readonly IProductService _productService;
    //private readonly IStringLocalizer<SharedResource> _localizer;
    //private readonly IOrderService _orderService;
    //private readonly IPaymentService _paymentService;
    [Authorize(Roles = "Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public AdminController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        // ================= DASHBOARD =================
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsList(1, 5);
            var orders = await _orderService.GetAllOrdersForAdmin();

            ViewBag.TotalProducts = products.ModelList?.Count ?? 0;
            ViewBag.TotalOrders = orders.Count;
            ViewBag.TotalRevenue = orders.Where(o => o.Status == OrderStatus.Paid).Sum(o => o.TotalAmount);

            return View("Dashboard");
        }

        // ================= PRODUCTS =================
        [HttpGet("Products")]
        public async Task<IActionResult> Products(int page = 1, int pageSize = 10)
        {
            var result = await _productService.GetProductsList(page, pageSize);
            return View("Products", result.IsValid ? result.ModelList : new List<ProductDTO>());
        }

        [HttpGet("Products/Create")]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost("Products/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // 1. Map the View Model to the DTO
            // We pass the actual 'model.Image' (IFormFile) inside the DTO
            var dto = new ProductDTO
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Taste = model.Taste,
                Weight = model.Weight,
                Stock = model.Stock,
                DiscountPercentage = model.DiscountPercentage,
                Type = model.Type,
                ImageFile = model.Image // The actual file
            };

            // 2. Let the Service handle the Guid, FileStream, and DB save
            var result = await _productService.AddProduct(dto);

            if (result.IsValid)
            {
                TempData["Message"] = "Product created successfully";
                return RedirectToAction("Products");
            }

            ModelState.AddModelError("", result.ErrorMessage);
            return View(model);
        }

        [HttpGet("Products/Edit/{id}")]
        public async Task<IActionResult> EditProduct(Guid id)
        {
            var result = await _productService.GetProductById(id);
            if (!result.IsValid) return RedirectToAction("Products");

            var product = result.Model;
            var model = new EditProductViewModel
            {
                Id = product.Id.Value,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Taste = product.Taste,
                Weight = product.Weight,
                Stock = product.Stock,
                DiscountPercentage = product.DiscountPercentage,
                Type = product.Type,
                CurrentImage = product.ImageUrl
            };

            return View(model);
        }

        [HttpPost("Products/Edit/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // We no longer handle the image saving here. 
            // We just pass the 'NewImage' (IFormFile) to the DTO.
            var dto = new ProductDTO
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Taste = model.Taste,
                Weight = model.Weight,
                Stock = model.Stock,
                DiscountPercentage = model.DiscountPercentage,
                Type = model.Type,
                ImageFile = model.NewImage // Pass the raw file
            };

            var result = await _productService.UpdateProduct(dto);

            if (!result.IsValid)
            {
                ViewBag.ErrorMessage = result.ErrorMessage;
                return View(model);
            }

            TempData["Message"] = "Product updated successfully";
            return RedirectToAction("Products");
        }

        [HttpPost("Products/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProduct(id);

            if (result.IsValid)
            {
                TempData["Message"] = "Product and associated image deleted successfully.";
            }
            else
            {
                TempData["Error"] = result.ErrorMessage;
            }

            return RedirectToAction("Products");
        }
        // ================= ORDERS =================
        [HttpGet("Orders")]
        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetAllOrdersForAdmin();
            return View("Orders", orders);
        }

        [HttpGet("Orders/Details/{id}")]
        public async Task<IActionResult> OrderDetails(Guid id)
        {
            var order = await _orderService.GetOrderDetailsForAdmin(id);
            if (order == null) return NotFound();
            return View("OrderDetails", order);
        }
    }


    //   [HttpGet]
    //   public async Task<IActionResult> Index()
    //   {
    //       return View();  
    //   }

    //   [HttpGet]
    //   public async Task<IActionResult> GetProductsList(
    //int pageNumber = 1,
    //int pageSize = 6,
    //ProductType? productTypeFilter = null,
    //TypeSorting? sorting = null)
    //   {
    //       var result = await _productService.GetProductsList(pageNumber, pageSize, productTypeFilter, sorting);

    //       if (!result.IsValid || result.ModelList == null)
    //       {
    //           TempData["ErrorMessage"] = result.ErrorMessage;
    //           return RedirectToAction("Index");
    //       }

    //       var model = new ProductsListViewModel
    //       {
    //           Products = result.ModelList,
    //           TotalPages = result.TotalPages,
    //           CurrentPage = pageNumber
    //       };

    //       return View("ProductsList", model);
    //   }



    //   [HttpPost]
    //   public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
    //   {
    //       if (!ModelState.IsValid)
    //       {
    //           ViewBag.ErrorMessage = _localizer["InvalidData"];
    //           return View(productDTO);
    //       }

    //       var result = await _productService.AddProduct(productDTO);
    //       if (!result.IsValid)
    //       {
    //           ViewBag.ErrorMessage = result.ErrorMessage;
    //           return View(productDTO);
    //       }

    //       TempData["Message"] = "New product added";
    //       return RedirectToAction("Index");
    //   }

    //   [HttpPost]
    //   public async Task<IActionResult> UpdateProduct(ProductDTO productDTO)
    //   {
    //       if (!ModelState.IsValid)
    //       {
    //           ViewBag.ErrorMessage = _localizer["InvalidData"];
    //           return View(productDTO);
    //       }

    //       var result = await _productService.UpdateProduct(productDTO);
    //       if (!result.IsValid)
    //       {
    //           ViewBag.ErrorMessage = result.ErrorMessage;
    //           return View(productDTO);
    //       }

    //       TempData["Message"] = "Update is done";
    //       return RedirectToAction("Index");
    //   }

    //   [HttpPost]
    //   public async Task<IActionResult> DeleteProduct(Guid id)
    //   {
    //       var result = await _productService.DeleteProduct(id);
    //       TempData[result.IsValid ? "Message" : "ErrorMessage"] = result.IsValid ? "Product has been deleted" : result.ErrorMessage;
    //       return RedirectToAction("Index");
    //   }





}

