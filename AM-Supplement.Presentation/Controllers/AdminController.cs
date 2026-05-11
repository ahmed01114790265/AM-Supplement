using AM_Supplement.Contracts.DTO.AdminDasboard;
using AM_Supplement.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AM_Supplement.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public AdminController(
            IOrderService orderService,
            IUserService userService,
            IProductService productService)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
        }

        // Dashboard Overview
        [HttpGet("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var orders = await _orderService.GetAllOrders();
            var users = await _userService.GetAllUsers();
            var products = await _productService.GetProductsList();

            var model = new AdminDashboardDTO
            {
                Orders = orders,
                Users = users,
                Products = (IEnumerable<ProductDTO>)products
            };
            return View(model);
        }

        // Order Details
        [HttpGet("OrderDetails/{orderId}")]
        public async Task<IActionResult> OrderDetails(Guid orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            return View(order);
        }

        // User Management
        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllUsers();
            return View(users);
        }

        [HttpPost("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(Guid userId, string role)
        {
            await _userService.UpdateUserRole(userId, role);
            return RedirectToAction("Users");
        }

        // Product Management
        [HttpGet("Products")]
        public async Task<IActionResult> Products()
        {
            var products = await _productService.GetProductsList();
            return View(products);
        }

        [HttpGet("AddProduct")]
        public IActionResult AddProduct() => View();

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO product)
        {
            if (!ModelState.IsValid) return View(product);
            await _productService.AddProduct(product);
            return RedirectToAction("Products");
        }

        [HttpGet("EditProduct/{productId}")]
        public async Task<IActionResult> EditProduct(Guid productId)
        {
            var product = await _productService.GetProductById(productId);
            return View(product);
        }

        [HttpPost("EditProduct")]
        public async Task<IActionResult> EditProduct(ProductDTO product)
        {
            if (!ModelState.IsValid) return View(product);
            await _productService.UpdateProduct(product);
            return RedirectToAction("Products");
        }

        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await _productService.DeleteProduct(productId);
            return RedirectToAction("Products");
        }
    }

}
