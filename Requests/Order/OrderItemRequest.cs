

using MovieApp.Enums;

namespace MovieApp.Requests.Order;

public class OrderItemRequest
{
    public int Qty { get; set; }
    public long MovieScheduleId { get; set; }
}