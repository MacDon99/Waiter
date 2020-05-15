using Waiter.Services.Interfaces;
using Waiter.ViewModels;

namespace Waiter.Services
{
    public class PayService : IPayService
    {
        public OrdersViewModel BeginPaymentProcess(OrdersViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public PayViewModel FinalizePayment(PayViewModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}