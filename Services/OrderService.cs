using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Waiter.Models;
using Waiter.Services.Interfaces;
using Waiter.ViewModels;

namespace Waiter.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpContextAccessor _httpContext;
        public OrderService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public TableViewModel AddDishToOrder(TableViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public OrderViewModel CreateOrder(OrderViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public OrdersViewModel DeleteOrder(OrdersViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public TableViewModel RemoveDishFromOrder(DishViewModel model)
        {
            throw new System.NotImplementedException();
        }
        private List<string> getTables(OrderViewModel model)
        {
            var listToPass = new List<string>();
            if(model.TableFirst)
            {
                listToPass.Add("First");
            }
            if(model.TableSecond)
            {
                listToPass.Add("Second");
            }
            if(model.TableThird)
            {
                listToPass.Add("Third");
            }
            if(model.TableFourth)
            {
                listToPass.Add("Fourth");
            }
            if(model.TableFifth)
            {
                listToPass.Add("Fifth");
            }
            return listToPass;
        }
    }
}