using System.Collections.Generic;

namespace Waiter.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string DishName { get; set;}
        public decimal DishPrice { get; set; }
        public int Count { get; set; }

        public Dish()
        {
        }
    }
}