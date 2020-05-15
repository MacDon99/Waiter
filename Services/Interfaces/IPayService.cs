using Waiter.ViewModels;

namespace Waiter.Services.Interfaces
{
    public interface IPayService
    {
         OrdersViewModel BeginPaymentProcess(OrdersViewModel model);
         PayViewModel FinalizePayment(PayViewModel model);
    }
}