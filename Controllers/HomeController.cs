using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Waiter.Data;
using Waiter.Models;

namespace Waiter.Controllers
{
    public class HomeController : Controller
    {
        private readonly Seeder _seeder;
        public HomeController(Seeder seeder)
        {
            _seeder = seeder;
        }
        [HttpGet]
        public IActionResult Index(TableViewModel model)
        {
            _seeder.Seed(this);
            return View();
        }

        public IActionResult Privacy()
        {
            _seeder.Seed(this);
            return View();
        }

        [HttpPost]
        public IActionResult GetTableInfo(TableViewModel model)
        {
            if(model.TableJson != null)
            {
                var table = JsonConvert.DeserializeObject<Table>(model.TableJson);
                HttpContext.Session.SetString(table.TableName, JsonConvert.SerializeObject(table));
            }
            _seeder.Seed(this);
            if(model != null && model.TableName != null && model.TableName != "Select Table")
            {
            Table selectedTable = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
            model.Table = selectedTable;
            model.TableJson = JsonConvert.SerializeObject(model.Table);
            }
            return View("Index", model);
        }
        [HttpPost]
        public IActionResult AddDishToOrder(TableViewModel model)
        {
            _seeder.Seed(this);
            if(model.TableJson != null)
                model.Table = JsonConvert.DeserializeObject<Table>(model.TableJson);

            if(model != null && model.DishName != null && model.DishName != "Select Dish")
            {
                if(!model.Table.Order.Dishes.Any(d => d.DishName == model.DishName))
                {
                    Dish selectedDish = JsonConvert.DeserializeObject<Dish>(HttpContext.Session.GetString(model.DishName));
                    model.Table.Order.AddDishToOrder(selectedDish, model.Quantity);
                } else {
                    model.Table.Order.Dishes.FirstOrDefault(d => d.DishName == model.DishName).Count+=model.Quantity;
                    model.Table.Order.OrderPrice += model.Table.Order.Dishes.FirstOrDefault(d => d.DishName == model.DishName).DishPrice*model.Quantity;
                }
            }
            model.TableJson = JsonConvert.SerializeObject(model.Table);
            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
