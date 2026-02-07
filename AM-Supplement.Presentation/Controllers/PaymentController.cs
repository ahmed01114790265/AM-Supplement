using AM_Supplement.Contracts.DTO;
using AM_Supplement.Contracts.Services;
using AMSupplement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AM_Supplement.Presentation.Controllers
{
    [Authorize]
    [Route("Payment")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(
            IPaymentService paymentService,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _userManager = userManager;
        }

        private async Task<Guid> GetUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        // 🔹 Checkout Page
        [HttpGet("Checkout")]
        public async Task<IActionResult> Checkout()
        {
            var cart = await _orderService.GetCart(await GetUserId());

            if (cart == null || !cart.Items.Any())
                return RedirectToAction("Index", "Cart");

            return View(cart); // Views/Payment/Checkout.cshtml
        }

        // 🔹 Pay (Mock / Stripe later)
        [HttpPost("Pay")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay()
        {
            var userId = await GetUserId();

            var result = await _paymentService.PayAsync(new PaymentRequestDTO
            {
                PaymentMethod = "Mock"
            });

            if (!result.Success)
                return RedirectToAction("Checkout");

            await _orderService.CompleteOrder(userId);
            return RedirectToAction("Success");
        }

        // 🔹 Success Page
        [HttpGet("Success")]
        public IActionResult Success()
        {
            return View(); // Views/Payment/Success.cshtml
        }
    }


}
