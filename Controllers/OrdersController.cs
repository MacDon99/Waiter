using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Waiter.Data;
using Waiter.Models;
using Waiter.Services.Interfaces;
using Waiter.ViewModels;

namespace Waiter.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IOrderService _orderService;
        private readonly IPayService _payService;
        public OrdersController(IDataService dataService, IOrderService orderService, IPayService payService)
        {
            _dataService = dataService;
            _orderService = orderService;
            _payService = payService;
            _dataService.CreateTablesAndDishes();
        }
        [HttpGet]
        public IActionResult Index()
        {
            var orderVM = new OrdersViewModel(){
                Orders = _dataService.GetAllTables()
            };
            return View(orderVM);
        }
        [HttpGet]
        public IActionResult CreateOrder()
        {
             return View();
        }
        [HttpPost]
        public IActionResult DeleteOrder(OrdersViewModel model)
        {
            _orderService.DeleteOrder(model);
             return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult BeginPaymentProcess(OrdersViewModel model)
        {
            return View("Payment",_payService.BeginPaymentProcess(model));

        }
        [HttpPost]
        public IActionResult FinalizePayment(PayViewModel model)
        {
            _payService.FinalizePayment(model);
            return View("PaymentCompleted");
        }
        [HttpPost]
        public IActionResult CreateOrder(OrderViewModel model)
        {
            _orderService.CreateOrder(model);
             return RedirectToAction("Index");
        }
    }
}
