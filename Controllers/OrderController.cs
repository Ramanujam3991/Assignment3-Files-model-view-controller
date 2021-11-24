using CandyShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thank you for your order. Enjoy your candy";
            return View();
        }

        public IActionResult History()
        {
            IEnumerable<Order> orders = _orderRepository.GetAllOrders;
            return View(orders);
        }

        public IActionResult EditHistory(int id)
        {
            Order order = (Order)_orderRepository.GetAllOrders.FirstOrDefault(c => c.OrderId == id);
            return View(order);
        }

        public IActionResult CancelHistory(int id)
        {
            Order order = (Order)_orderRepository.GetAllOrders.FirstOrDefault(c => c.OrderId == id);
            order.Status = "Cancelled";
            _orderRepository.SaveOrder(order);
            return RedirectToAction("History");
        }

        public IActionResult DeleteHistory(int id)
        {
            _orderRepository.DeleteOrder(id);
            return RedirectToAction("History");
        }
    }
}
