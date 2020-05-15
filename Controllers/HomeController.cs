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
    public class HomeController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IOrderService _orderService;
        private readonly IWaiterService _waiterService;
        public HomeController(IDataService dataService, IOrderService orderService, IWaiterService waiterService)
        {
            _dataService = dataService;
            _orderService = orderService;
            _waiterService = waiterService;
            _dataService.CreateTablesAndDishes();
        }
        [HttpGet]
        public IActionResult Index(TableViewModel model)
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetTableInfo(TableViewModel model)
        {
            var tableInformation = _dataService.GetTableInformation(model);
            return View("Index", tableInformation);
        }
        [HttpPost]
        public IActionResult AddDishToOrder(TableViewModel model)
        {
            var modelWithAddedDish = _orderService.AddDishToOrder(model);
            return View("Index", modelWithAddedDish);
        }
        [HttpPost]
        public IActionResult BeginChangingDish(DishViewModel model)
        {
            return View("DishDetails", model);
        }
        [HttpPost]
        public IActionResult ChangeDish(DishViewModel model)
        {
            var modelWithChangedDish = _waiterService.ChangeDish(model);
            return View("Details", modelWithChangedDish);
        }

        [HttpPost]
        public IActionResult EditDish(TableViewModel model)
        {
            var modelWithEditedDish = _waiterService.EditDish(model);
            return View("Details", modelWithEditedDish);
        }

        [HttpPost]
        public IActionResult AddOne(DishViewModel model)
        {
            var modelWithIncreasedPortions = _waiterService.IncreaseDishPortions(model);
            return View("Details", modelWithIncreasedPortions);
        }
        [HttpPost]
        public IActionResult RemoveOne(DishViewModel model)
        {
            var modelWithLoweredPortions = _waiterService.LowerDishPortions(model);
            return View("Details", modelWithLoweredPortions);
        }
        [HttpPost]
        public IActionResult RemoveDish(DishViewModel model)
        {
            var modelWithRemovedDish = _orderService.RemoveDishFromOrder(model);
            return View("Details", modelWithRemovedDish);
        }
    }
}
