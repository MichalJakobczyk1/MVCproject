using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MVCproject.Interfaces;
using MVCproject.Models;
using MVCproject.Repositories;
using MVCproject.Services;
using MVCproject.ViewModels;

namespace MVCproject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        { 
            IEnumerable<Order> orders = await _orderRepository.GetAll();
            return View(orders);
        }

        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(PlaceOrderViewModel placeOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                { 
                    DateOfOrder = DateTime.Now,
                    Quantity = placeOrderViewModel.Quantity,
                    IsPaid = false,
                };
                _orderRepository.Add(order);
                return RedirectToAction("Index");
            }
            return View(placeOrderViewModel);
        }
    }
}
