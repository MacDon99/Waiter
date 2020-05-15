using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Waiter.Models;
using Waiter.Services.Interfaces;
using Waiter.ViewModels;

namespace Waiter.Services
{
    public class DataService : IDataService
    {
        private readonly IHttpContextAccessor _httpContext;
        public DataService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public void CreateTablesAndDishes()
        {
            if(!DoTablesExist(_httpContext))
            {
                //tables
                _httpContext.HttpContext.Session.SetString("First", JsonConvert.SerializeObject( new Table(){TableName = "First"}));
                _httpContext.HttpContext.Session.SetString("Second", JsonConvert.SerializeObject( new Table(){TableName = "Second"}));
                _httpContext.HttpContext.Session.SetString("Third", JsonConvert.SerializeObject( new Table(){TableName = "Third"}));
                _httpContext.HttpContext.Session.SetString("Fourth", JsonConvert.SerializeObject( new Table(){TableName = "Fourth"}));
                _httpContext.HttpContext.Session.SetString("Fifth", JsonConvert.SerializeObject( new Table(){TableName = "Fifth"}));
                //food
                _httpContext.HttpContext.Session.SetString("Chicken with fries", JsonConvert.SerializeObject(new Dish(){DishName = "Chicken with fries", DishPrice = 16.5M}));
                _httpContext.HttpContext.Session.SetString("Fish with potatoes", JsonConvert.SerializeObject(new Dish(){DishName = "Fish with potatoes", DishPrice = 21.3M}));
                _httpContext.HttpContext.Session.SetString("Schnitzel with salad", JsonConvert.SerializeObject(new Dish(){DishName = "Schnitzel with salad", DishPrice = 19.2M}));
                _httpContext.HttpContext.Session.SetString("Hamburger", JsonConvert.SerializeObject(new Dish(){DishName = "Hamburger", DishPrice = 8.9M}));
                _httpContext.HttpContext.Session.SetString("Hot Dog", JsonConvert.SerializeObject(new Dish(){DishName = "Hot Dog", DishPrice = 4.5M}));
            }
        }

        public List<Table> GetAllTables()
        {
            throw new System.NotImplementedException();
        }

        public TableViewModel GetTableInformation(TableViewModel model)
        {
            throw new System.NotImplementedException();
        }
        private bool DoTablesExist(IHttpContextAccessor _httpContext)
        {
            if(_httpContext.HttpContext.Session.GetString("First") == null)
            {
                return false;
            }
            return true;
        }
    }
}