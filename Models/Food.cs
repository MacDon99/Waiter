using System.Collections.Generic;

namespace Waiter.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PortionCount { get; set; }
        public decimal PortionPrice { get; private set; }
        public decimal TotalPrice { get; set; }

        public Food(string name, decimal portionPrice)
        {
            Name =name;
            PortionPrice = portionPrice;
        }
        public void AddPortion()
        {
            PortionCount +=1;
            TotalPrice += PortionPrice;
        }
    }
}