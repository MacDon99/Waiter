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
                model.Table = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));
                
                foreach(var item in model.Table.Order.Dishes){
                    System.Console.WriteLine(item.DishName);
                    System.Console.WriteLine(item.Count);

                }

                var dishes = getDishes(model.DishSelect);

                if(dishes.Count != 0)
                {
                    foreach(var dish in dishes){
                        if(dish.Quantity != 0){
                          if(!model.Table.Order.Dishes.Any(d => d.DishName == dish.Name))
                            {
                                Dish selectedDish = JsonConvert.DeserializeObject<Dish>(_httpContext.HttpContext.Session.GetString(dish.Name));
                                model.Table.Order.AddDishToOrder(selectedDish, dish.Quantity);
                            } else {
                                model.Table.Order.Dishes.FirstOrDefault(d => d.DishName == dish.Name).Count+=dish.Quantity;
                                model.Table.Order.OrderPrice += model.Table.Order.Dishes.FirstOrDefault(d => d.DishName == dish.Name).DishPrice*dish.Quantity;
                            }
                        }
                    }
                }
            _httpContext.HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(model.Table));
            return model;
        }

        public void CreateOrder(OrderViewModel model)
        {

            var tables = getTables(model);
            var dishes = getDishes(model.DishSelect);

            if(tables != null)
            {
                if(dishes != null)
                {
                    foreach(var table in tables)
                    {
                        foreach(var dish in dishes)
                        {
                            var tableFromSession = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(table));
                            var dishFromSession = JsonConvert.DeserializeObject<Dish>(_httpContext.HttpContext.Session.GetString(dish.Name));
                            dishFromSession.Count = dish.Quantity;

                            if(tableFromSession.Order.Dishes.Any(d => d.DishName == dish.Name))
                            {
                                tableFromSession.Order.Dishes.FirstOrDefault(d => d.DishName == dish.Name).Count += dish.Quantity;
                                tableFromSession.Order.OrderPrice += dish.Quantity * dishFromSession.DishPrice;
                                _httpContext.HttpContext.Session.SetString(table, JsonConvert.SerializeObject(tableFromSession));
                            } else {
                                tableFromSession.Order.Dishes.Add(dishFromSession);
                                tableFromSession.Order.OrderPrice += dish.Quantity * dishFromSession.DishPrice;
                                _httpContext.HttpContext.Session.SetString(table, JsonConvert.SerializeObject(tableFromSession));
                            }
                        }
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
            if(model.TableSelect.isTableFirstChecked)
            {
                listToPass.Add("First");
            }
            if(model.TableSelect.isTableSecondChecked)
            {
                listToPass.Add("Second");
            }
            if(model.TableSelect.isTableThirdChecked)
            {
                listToPass.Add("Third");
            }
            if(model.TableSelect.isTableFourthChecked)
            {
                listToPass.Add("Fourth");
            }
            if(model.TableSelect.isTableFifthChecked)
            {
                listToPass.Add("Fifth");
            }
            return listToPass;
        }

        private List<DishToAdd> getDishes(DishSelectViewModel dishSelect)
        {
            var dishes = new List<DishToAdd>();
            if(dishSelect.isChickenSelected)
            {
                dishes.Add(new DishToAdd(){
                    Name = "Chicken with fries",
                    Quantity = dishSelect.ChickenQuantity
                });
            }
            if(dishSelect.isFishSelected){
                dishes.Add(new DishToAdd(){
                    Name = "Fish with potatoes",
                    Quantity = dishSelect.FishQuantity
                });
            }
            if(dishSelect.isSchnitzelSelected){
                dishes.Add(new DishToAdd(){
                    Name = "Schnitzel with salad",
                    Quantity = dishSelect.SchnitzelQuantity
                });
            }
            if(dishSelect.isHamburgerSelected){
                dishes.Add(new DishToAdd(){
                    Name = "Hamburger",
                    Quantity = dishSelect.HamburgerQuantity
                });
            }
            if(dishSelect.isHotDogSelected){
                dishes.Add(new DishToAdd(){
                    Name = "Hot Dog",
                    Quantity = dishSelect.HotDogQuantity
                });
            }
            return dishes;
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