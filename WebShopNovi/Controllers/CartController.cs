using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WebShopNovi.Models;
using WebShopNovi.Services;
using Microsoft.AspNetCore.Http;
using WebShopNovi.Extensions;

namespace WebShopNovi.Controllers
{
    public class CartController : Controller
    {

        private readonly ApplicationDbContext _context;
        private const string SessionCartKey = "_Cart";

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult AddToCart(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
                return NotFound();

            var cart = GetCart();

            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            SaveCart(cart);

            return RedirectToAction("Index");
        }

        private List<CartItem> GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionCartKey);
            return cart ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetObjectAsJson(SessionCartKey, cart);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var itemToRemove = cart.FirstOrDefault(c => c.ProductId == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult IncreaseQuantity(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                item.Quantity++;
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult DecreaseQuantity(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    cart.Remove(item);
                }
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }
    }

    
}
