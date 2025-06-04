using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;
using WebShopNovi.Models;
using WebShopNovi.Services;
using System.Globalization;
using System.Runtime.CompilerServices;
using WebShopNovi.Extensions;

namespace WebShopNovi.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly PayPalClient _payPalClient;
        private const string SessionCartKey = "_Cart";

        public CheckoutController(PayPalClient payPalClient)
        {
            _payPalClient = payPalClient;
        }

        private List<CartItem>GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionCartKey);
            return cart ?? new List<CartItem>();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var cartItems = GetCart();
            decimal total = cartItems.Sum(item => item.Price * item.Quantity);
            string totalString = total.ToString("F2", CultureInfo.InvariantCulture);

            var orderRequest = new OrdersCreateRequest();
            orderRequest.Prefer("return=representation");
            orderRequest.RequestBody(new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest {
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = "EUR",
                            Value = totalString
                        }
                    }
                }
            });

            var response = await _payPalClient.Client.Execute(orderRequest);
            var result = response.Result<Order>();

            return Json(new { id = result.Id });
        }

        [HttpPost]
        public async Task<IActionResult> CaptureOrder([FromBody] string orderId)
        {
            var request = new OrdersCaptureRequest(orderId);
            request.RequestBody(new OrderActionRequest());

            var response = await _payPalClient.Client.Execute(request);
            var result = response.Result<Order>();

            return Json(result);
        }
    }
}
