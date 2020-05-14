using Newtonsoft.Json;

namespace Waiter.Models
{
    public class TableViewModel
    {
        public string TableName { get; set; }
        public string DishName { get; set; }
        public int Quantity { get; set; }
        public Table Table { get; set; }

        public string TableJson { get; set; }

        public TableViewModel()
        {

        }
    }
}