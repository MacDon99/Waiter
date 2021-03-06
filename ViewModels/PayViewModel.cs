using Waiter.Models;

namespace Waiter.ViewModels
{
    public class PayViewModel
    {
        public string TableName { get; set; }
        public Order Order { get; set; }
        public decimal Price { get; set; }
        public decimal Tip { get; set; }
    }
}