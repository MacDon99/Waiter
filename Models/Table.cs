using System.Collections.Generic;

namespace Waiter.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public Order Order { get; set; }
        public decimal TotalPrice { get; set; }

        public Table()
        {
            Order = new Order();
        }
        public void AddOrder(Order order)
        {
            TotalPrice += order.OrderPrice;
        }
    }
}