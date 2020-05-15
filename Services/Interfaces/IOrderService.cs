using System.Collections.Generic;
using Waiter.Models;
using Waiter.ViewModels;

namespace Waiter.Services.Interfaces
{
    public interface IOrderService
    {
         void DeleteOrder(OrdersViewModel model);
         void CreateOrder(OrderViewModel model);
         DishViewModel RemoveDishFromOrder(DishViewModel model);
         TableViewModel AddDishToOrder(TableViewModel model);
    }
}