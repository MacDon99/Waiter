using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Waiter.Models;

namespace Waiter.Data
{
    public class Seeder
    {
        public bool Seed(ControllerBase controller)
        {
            if(!DoTablesExist(controller))
            {
                //tables
                controller.HttpContext.Session.SetString("First", JsonConvert.SerializeObject( new Table(){TableName = "First"}));
                controller.HttpContext.Session.SetString("Second", JsonConvert.SerializeObject( new Table(){TableName = "Second"}));
                controller.HttpContext.Session.SetString("Third", JsonConvert.SerializeObject( new Table(){TableName = "Third"}));
                controller.HttpContext.Session.SetString("Fourth", JsonConvert.SerializeObject( new Table(){TableName = "Fourth"}));
                controller.HttpContext.Session.SetString("Fifth", JsonConvert.SerializeObject( new Table(){TableName = "Fifth"}));
                //food
                controller.HttpContext.Session.SetString("Chicken with fries", JsonConvert.SerializeObject(new Dish(){DishName = "Chicken with fries", DishPrice = 16.5M}));
                controller.HttpContext.Session.SetString("Fish with potatoes", JsonConvert.SerializeObject(new Dish(){DishName = "Fish with potatoes", DishPrice = 21.3M}));
                controller.HttpContext.Session.SetString("Schnitzel with salad", JsonConvert.SerializeObject(new Dish(){DishName = "Schnitzel with salad", DishPrice = 19.2M}));
                controller.HttpContext.Session.SetString("Hamburger", JsonConvert.SerializeObject(new Dish(){DishName = "Hamburger", DishPrice = 8.9M}));
                controller.HttpContext.Session.SetString("Hot Dog", JsonConvert.SerializeObject(new Dish(){DishName = "Hot Dog", DishPrice = 4.5M}));
            }
            return true;
        }

        private bool DoTablesExist(ControllerBase controller)
        {
            if(controller.HttpContext.Session.GetString("First") == null)
            {
                return false;
            }
            return true;
        }
    }
}