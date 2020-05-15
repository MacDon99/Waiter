using System.Collections.Generic;
using Waiter.Models;

namespace Waiter.ViewModels
{
    public class OrdersViewModel
    {
        public List<Table> Orders { get; set; }
        public string TableName { get; set; }
    }
}