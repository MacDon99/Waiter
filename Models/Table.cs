using System.Collections.Generic;

namespace Waiter.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public Order Order { get; set; }
        public decimal Price { get; set; }
        public decimal Tip { get; set; }

        public Table()
        {
            Order = new Order();
        }
        public void AddOrder(Order order)
        {
            Price += order.OrderPrice;
        }
    }
}