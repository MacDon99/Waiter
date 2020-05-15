using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Waiter.Models;
using Waiter.Services.Interfaces;
using Waiter.ViewModels;

namespace Waiter.Services
{
    public class WaiterService : IWaiterService
    {
        private readonly IHttpContextAccessor _httpContext;
        public WaiterService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public DishViewModel ChangeDish(DishViewModel model)
        {
            var tableFromSession = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));
            tableFromSession.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName).DishName = model.NewDish;
            _httpContext.HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(tableFromSession));
            model.Dish = tableFromSession.Order.Dishes.FirstOrDefault(d => d.DishName == model.NewDish);
            return model;
        }

        public DishViewModel EditDish(TableViewModel model)
        {
            model.Table = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));

            var dishVM = new DishViewModel(){
                Dish = model.Table.Order.Dishes.FirstOrDefault(d => d.DishName == model.DishName),
                TableName = model.Table.TableName
            };
            _httpContext.HttpContext.Session.SetString(model.Table.TableName, JsonConvert.SerializeObject(model.Table));
            return dishVM;
        }

        public DishViewModel IncreaseDishPortions(DishViewModel model)
        {
            var table = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));
            table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName).Count++;
            table.Order.OrderPrice += table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName).DishPrice;
            _httpContext.HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(table));
            model.Dish.Count++;
            return model;
        }

        public DishViewModel LowerDishPortions(DishViewModel model)
        {
            var table = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));
            if(model.Dish.Count > 0)
            {
            table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName).Count--;
            table.Order.OrderPrice -= table.Order.Dishes.FirstOrDefault(d => d.DishName == model.Dish.DishName).DishPrice;
            _httpContext.HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(table));
            model.Dish.Count--;
            }
            return model;
        }
    }
}