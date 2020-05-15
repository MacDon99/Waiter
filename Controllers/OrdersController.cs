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
    public class OrdersController : Controller
    {
        public OrdersController()
        {
        }
        [HttpGet]
        public IActionResult Index()
        {
            var orders = new List<Table>(){
                    JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString("First")),
                    JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString("Second")),
                    JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString("Third")),
                    JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString("Fourth")),
                    JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString("Fifth")),
                };
            var orderVM = new OrdersViewModel(){
                Orders = orders,
                OrdersJson = JsonConvert.SerializeObject(orders)
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
            var tableFromSession = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
            tableFromSession.Order = new Order();
            HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(tableFromSession));
             return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult BeginPaymentProcess(OrdersViewModel model)
        {
            var tableFromSession = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
            var tip = Math.Round(tableFromSession.Order.OrderPrice * 0.05M, 2);
            return View("Payment", new PayViewModel(){
                TableName = tableFromSession.TableName,
                Order = tableFromSession.Order,
                Tip = tip,
                Price = tableFromSession.Order.OrderPrice + tip
            });
        }
        [HttpPost]
        public IActionResult FinalizePayment(PayViewModel model)
        {
            var tableFromSession = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(model.TableName));
            tableFromSession.Order = new Order();
            HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(tableFromSession));
            return View("PaymentCompleted");
        }
        [HttpPost]
        public IActionResult CreateOrder(OrderViewModel model)
        {
            var dishFromSession = JsonConvert.DeserializeObject<Dish>(HttpContext.Session.GetString(model.DishName));
            dishFromSession.Count = model.Quantity;

            List<string> tables = new List<string>();

            tables = getTables(model);

            if(tables != null)
            {
                foreach(string table in tables)
                {
                    var tableFromSession = JsonConvert.DeserializeObject<Table>(HttpContext.Session.GetString(table));

                    if(tableFromSession.Order.Dishes.Any(d => d.DishName == model.DishName))
                    {
                        tableFromSession.Order.Dishes.FirstOrDefault(d => d.DishName == model.DishName).Count += model.Quantity;
                        tableFromSession.Order.OrderPrice += model.Quantity * dishFromSession.DishPrice;
                        HttpContext.Session.SetString(table, JsonConvert.SerializeObject(tableFromSession));
                    } else {
                        tableFromSession.Order.Dishes.Add(dishFromSession);
                        tableFromSession.Order.OrderPrice += model.Quantity * dishFromSession.DishPrice;
                        HttpContext.Session.SetString(table, JsonConvert.SerializeObject(tableFromSession));
                    }
                }
            }
             return RedirectToAction("Index");
        }

        private List<string> getTables(OrderViewModel model)
        {
            var listToPass = new List<string>();
            if(model.TableFirst)
            {
                listToPass.Add("First");
            }
            if(model.TableSecond)
            {
                listToPass.Add("Second");
            }
            if(model.TableThird)
            {
                listToPass.Add("Third");
            }
            if(model.TableFourth)
            {
                listToPass.Add("Fourth");
            }
            if(model.TableFifth)
            {
                listToPass.Add("Fifth");
            }
            return listToPass;
        }
    }
}
