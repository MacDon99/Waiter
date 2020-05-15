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
            HttpContext.Session.SetString(model.TableName, model.TableJson);
            return View("Index", model);
        }
        [HttpPost]
        public IActionResult BeginChangingDish(DishViewModel model)
        {
            return View("DishDetails", model);
        }
        [HttpPost]
        public IActionResult ChangeDish(DishViewModel model)
        {
            var tableFromSession = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
            tableFromSession.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName).DishName = model.NewDish;
            HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(tableFromSession));
            model.Dish = tableFromSession.Order.Dishes.FirstOrDefault(d => d.DishName == model.NewDish);
            return View("Details", model);
        }

        [HttpPost]
        public IActionResult EditDish(TableViewModel model)
        {
            _seeder.Seed(this);
            if(model.TableJson != null)
                model.Table = JsonConvert.DeserializeObject<Table>(model.TableJson);
            var dishVM = new DishViewModel(){
                Dish = model.Table.Order.Dishes.FirstOrDefault(d => d.DishName == model.DishName),
                TableName = model.Table.TableName
            };
            HttpContext.Session.SetString(model.Table.TableName, JsonConvert.SerializeObject(model.Table));
            return View("Details", dishVM);
        }

        [HttpPost]
        public IActionResult AddOne(DishViewModel model)
        {
            _seeder.Seed(this);
            var table = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
            table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName).Count++;
            table.Order.OrderPrice += table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName).DishPrice;
            HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(table));
            model.Dish.Count++;
            return View("Details", model);
        }
        [HttpPost]
        public IActionResult RemoveOne(DishViewModel model)
        {
            _seeder.Seed(this);
            var table = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
            if(model.Dish.Count > 0)
            {
            table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName).Count--;
            table.Order.OrderPrice -= table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName).DishPrice;
            HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(table));
            model.Dish.Count--;
            }
            return View("Details", model);
        }
        [HttpPost]
        public IActionResult RemoveDish(DishViewModel model)
        {
            _seeder.Seed(this);
            
            var table = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
            var dish = table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName);
            table.Order.Dishes.Remove(dish);
            table.Order.OrderPrice -= dish.Count*dish.DishPrice;
            // table.Order.Dishes.RemoveAt(dishIndex);

            HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(table));
            model.Message = "You have succesfully removed the dish";

            return View("Details", model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
