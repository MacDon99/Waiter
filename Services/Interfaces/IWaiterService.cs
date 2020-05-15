using Waiter.ViewModels;

namespace Waiter.Services.Interfaces
{
    public interface IWaiterService
    {
         DishViewModel IncreaseDishPortions(DishViewModel model);
         DishViewModel LowerDishPortions(DishViewModel model);
         DishViewModel ChangeDish(DishViewModel model);
         TableViewModel EditDish(TableViewModel model);

    }
}