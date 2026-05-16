using OnlineBookstoreApp.Models;

namespace OnlineBookstoreApp.Services;

public class OrderService(CartSessionService cartSessionService)
{
    public OrderSummary GetSummary()
    {
        return new OrderSummary
        {
            Items = cartSessionService.GetCart()
        };
    }

    public OrderSummary ConfirmOrder()
    {
        var summary = GetSummary();
        cartSessionService.Clear();
        return summary;
    }
}
