using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Waiter.Models;

namespace Waiter.Data
{
    public class Seeder
    {
        private readonly IHttpContextAccessor _httpContext;
        public Seeder(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public bool Seed()
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
            return true;
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