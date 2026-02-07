using AM_Supplement.Contracts.Services;
using AMSupplement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AM_Supplement.Presentation.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        private async Task<Guid> GetUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrderHistory(await GetUserId());
            return View(orders);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var order = await _orderService.GetOrderDetails(id, await GetUserId());
            if (order == null) return NotFound();

            return View(order);
        }
    }

}
