using System.Collections.Generic;
using Waiter.Models;
using Waiter.ViewModels;

namespace Waiter.Services.Interfaces
{
    public interface IOrderService
    {
         OrdersViewModel DeleteOrder(OrdersViewModel model);
         OrderViewModel CreateOrder(OrderViewModel model);
        TableViewModel RemoveDishFromOrder(DishViewModel model);
        TableViewModel AddDishToOrder(TableViewModel model);
    }
}