using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Waiter.Data;
using Waiter.Models;
using Waiter.ViewModels;

namespace Waiter.Controllers
{
    public class HomeController : Controller
    {
        private readonly Seeder _seeder;
        public HomeController(Seeder seeder)
        {
            _seeder = seeder;
            _seeder.Seed();
        }
        [HttpGet]
        public IActionResult Index(TableViewModel model)
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetTableInfo(TableViewModel model)
        {
                var tableFromSession = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));

            if(model != null && model.TableName != null && model.TableName != "Select Table")
            {
            Table selectedTable = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
            model.Table = selectedTable;
            }
            return View("Index", model);
        }
        [HttpPost]
        public IActionResult AddDishToOrder(TableViewModel model)
        {
                var tableFromSession = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
                model.Table = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));

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
            HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(model.Table));
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
                model.Table = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));

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
            
            var table = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
            var dish = table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName);
            table.Order.Dishes.Remove(dish);
            table.Order.OrderPrice -= dish.Count*dish.DishPrice;

            HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(table));
            model.Message = "You have succesfully removed the dish";

            return View("Details", model);
        }
    }
}
