using System.Collections.Generic;

namespace Waiter.Models
{
    public class OrdersViewModel
    {
        public List<Table> Orders { get; set; }
        public string OrdersJson { get; set; }
        public string TableName { get; set; }
    }
}