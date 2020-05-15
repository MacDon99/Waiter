using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Waiter.Models;
using Waiter.Services.Interfaces;
using Waiter.ViewModels;

namespace Waiter.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpContextAccessor _httpContext;
        public OrderService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public TableViewModel AddDishToOrder(TableViewModel model)
        {
            var tableFromSession = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));
                model.Table = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));

            if(model != null && model.DishName != null && model.DishName != "Select Dish")
            {
                if(!model.Table.Order.Dishes.Any(d => d.DishName == model.DishName))
                {
                    Dish selectedDish = JsonConvert.DeserializeObject<Dish>(_httpContext.HttpContext.Session.GetString(model.DishName));
                    model.Table.Order.AddDishToOrder(selectedDish, model.Quantity);
                } else {
                    model.Table.Order.Dishes.FirstOrDefault(d => d.DishName == model.DishName).Count+=model.Quantity;
                    model.Table.Order.OrderPrice += model.Table.Order.Dishes.FirstOrDefault(d => d.DishName == model.DishName).DishPrice*model.Quantity;
                }
            }
            _httpContext.HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(model.Table));
            return model;
        }

        public void CreateOrder(OrderViewModel model)
        {

            var dishFromSession = JsonConvert.DeserializeObject<Dish>(_httpContext.HttpContext.Session.GetString(model.DishName));

                dishFromSession.Count = model.Quantity;

            List<string> tables = getTables(model);

            if(tables != null)
            {
                foreach(string table in tables)
                {
                    var tableFromSession = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(table));

                    if(tableFromSession.Order.Dishes.Any(d => d.DishName == model.DishName))
                    {
                        tableFromSession.Order.Dishes.FirstOrDefault(d => d.DishName == model.DishName).Count += model.Quantity;
                        tableFromSession.Order.OrderPrice += model.Quantity * dishFromSession.DishPrice;
                        _httpContext.HttpContext.Session.SetString(table, JsonConvert.SerializeObject(tableFromSession));
                    } else {
                        tableFromSession.Order.Dishes.Add(dishFromSession);
                        tableFromSession.Order.OrderPrice += model.Quantity * dishFromSession.DishPrice;
                        _httpContext.HttpContext.Session.SetString(table, JsonConvert.SerializeObject(tableFromSession));
                    }
                }
            }
        }

        public void DeleteOrder(OrdersViewModel model)
        {
            var tableFromSession = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));
            tableFromSession.Order = new Order();
            _httpContext.HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(tableFromSession));
        }

        public DishViewModel RemoveDishFromOrder(DishViewModel model)
        {
            var table = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));
            var dish = table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName);
            table.Order.Dishes.Remove(dish);
            table.Order.OrderPrice -= dish.Count*dish.DishPrice;

            _httpContext.HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(table));
            model.Message = "You have succesfully removed the dish";

            return model;
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
        private bool isItPossibleToGetIntFromRequestedVal(string value)
        {
            try
            {
                Convert.ToInt32(value);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}