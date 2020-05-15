using Waiter.Models;

namespace Waiter.ViewModels
{
    public class DishViewModel
    {
        public Dish Dish { get; set; }
        public string Message { get; set; }
        public string TableName { get; set; }
        public string NewDish { get; set; }
    }
}