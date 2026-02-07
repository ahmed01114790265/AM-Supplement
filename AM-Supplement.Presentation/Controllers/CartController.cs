using AM_Supplement.Contracts.Services;
using AMSupplement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AM_Supplement.Presentation.Controllers
{
    [Authorize]
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        private async Task<Guid> GetUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        // 🛒 Cart Page
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var cart = await _orderService.GetCart(await GetUserId());
            return View(cart); // Views/Cart/Index.cshtml
        }

        // ➕ Add to cart
        [HttpPost("Add")]
        public async Task<IActionResult> Add(Guid productId, int quantity = 1)
        {
            await _orderService.AddToCart(await GetUserId(), productId, quantity);
            return RedirectToAction("Index");
        }

        // ❌ Remove
        [HttpPost("Remove")]
        public async Task<IActionResult> Remove(Guid productId)
        {
            await _orderService.RemoveFromCart(await GetUserId(), productId);
            return RedirectToAction("Index");
        }

        // 🔄 Update quantity
        [HttpPost("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity(Guid productId, int quantity)
        {
            await _orderService.UpdateQuantity(await GetUserId(), productId, quantity);
            return RedirectToAction("Index");
        }
    }

}