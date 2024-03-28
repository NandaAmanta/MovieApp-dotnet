

using MovieApp.Enums;

namespace MovieApp.Requests.Order;

public class OrderItemRequest
{
    public int Qyt { get; set; }
    public long MovieScheduleId { get; set; }
}