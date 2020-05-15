using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Waiter.Models;
using Waiter.Services.Interfaces;
using Waiter.ViewModels;

namespace Waiter.Services
{
    public class PayService : IPayService
    {
        private readonly IHttpContextAccessor _httpContext;
        public PayService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public PayViewModel BeginPaymentProcess(OrdersViewModel model)
        {
            var tableFromSession = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));
            var tip = Math.Round(tableFromSession.Order.OrderPrice * 0.05M, 2);
            return new PayViewModel(){
                TableName = tableFromSession.TableName,
                Order = tableFromSession.Order,
                Tip = tip,
                Price = tableFromSession.Order.OrderPrice + tip
            };
        }

        public void FinalizePayment(PayViewModel model)
        {
            var tableFromSession = JsonConvert.DeserializeObject<Table>(_httpContext.HttpContext.Session.GetString(model.TableName));
            tableFromSession.Order = new Order();
            _httpContext.HttpContext.Session.SetString(model.TableName, JsonConvert.SerializeObject(tableFromSession));
        }
    }
}