

using MovieApp.Enums;

namespace MovieApp.Requests.Order;

public class OrderCreationRequest
{
    public PaymentMethod PaymentMethod { get; set; }
    public List<OrderItemRequest> OrderItems { get; set; } = [];
}