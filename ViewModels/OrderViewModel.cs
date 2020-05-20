namespace Waiter.ViewModels
{
    public class OrderViewModel
    {
        public string DishName { get; set; }
        public int Quantity { get; set; }
        public TableSelectViewModel TableSelect { get; set; }
        public DishSelectViewModel DishSelect { get; set; }
    }
}