using Waiter.ViewModels;

namespace Waiter.Services.Interfaces
{
    public interface IPayService
    {
         PayViewModel BeginPaymentProcess(OrdersViewModel model);
         void FinalizePayment(PayViewModel model);
    }
}