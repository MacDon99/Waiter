using System.Collections.Generic;
using System.Linq;

namespace Waiter.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Dish> Dishes { get; set; }
        public decimal OrderPrice { get; set; }

        public Order()
        {
            Dishes = new List<Dish>();
        }

        public void AddDishToOrder(Dish dish, int quantity)
        {
            Dishes.Add(dish);
            OrderPrice += dish.DishPrice*quantity;
            Dishes.FirstOrDefault(d => d.DishName == dish.DishName).Count += quantity;
        }
    }
}