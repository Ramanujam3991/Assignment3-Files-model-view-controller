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
    public class ManagerController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly ICandyRepository _candyRepository;
        private readonly ICategoryRepository _categoryRepository;
        IEnumerable<Candy> candies;

        public ManagerController(IOrderRepository orderRepository, ShoppingCart shoppingCart, ICandyRepository candyRepository, ICategoryRepository categoryRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _candyRepository = candyRepository;
            _categoryRepository = categoryRepository;
        }

       

        public IActionResult ManagerPanel()
        {
            IEnumerable<Order> orders = _orderRepository.GetAllOrders;
            return View(orders);
        }


        public IActionResult CancelOrder(int id)
        {
            Order order = (Order)_orderRepository.GetAllOrders.FirstOrDefault(c => c.OrderId == id);
            order.Status = "Cancelled";
            _orderRepository.SaveOrder(order);
            return RedirectToAction("ManagerPanel");
        }

        public IActionResult ProcessOrder(int id)
        {
            Order order = (Order)_orderRepository.GetAllOrders.FirstOrDefault(c => c.OrderId == id);
            order.Status = "Completed";
            _orderRepository.SaveOrder(order);
            return RedirectToAction("ManagerPanel");
        }

        public IActionResult ManageCandies()
        {
            candies = _candyRepository.GetAllCandy.OrderBy(c => c.CandyId);
            return View(candies);
        }

        public IActionResult Create()
        {
            //candies = _candyRepository.GetAllCandy.OrderBy(c => c.CandyId);
            //CreateCandy
            return View(new Candy());
        }

        public IActionResult Edit(int id)
        {
            Candy candy = (Candy)_candyRepository.GetAllCandy.FirstOrDefault(c => c.CandyId == id);
            return View(candy);
        }

        public IActionResult Delete(int id)
        {
            //candies = _candyRepository.GetAllCandy.OrderBy(c => c.CandyId);
            //CreateCandy
            return View(new Candy());
        }




    }
}
