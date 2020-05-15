namespace Waiter.ViewModels
{
    public class OrderViewModel
    {
        public string DishName { get; set; }
        public int Quantity { get; set; }
        public bool TableFirst { get; set; }
        public bool TableSecond { get; set; }
        public bool TableThird { get; set; }
        public bool TableFourth { get; set; }
        public bool TableFifth { get; set; }

        public OrderViewModel()
        {
        }
    }
}