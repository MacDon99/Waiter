using Waiter.Models;

namespace Waiter.ViewModels
{
    public class TableViewModel
    {
        public string TableName { get; set; }
        public string DishName { get; set; }
        public Table Table { get; set; }

        public DishSelectViewModel DishSelect { get; set; }
    }
}