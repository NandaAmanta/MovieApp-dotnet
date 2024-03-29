

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MovieApp.Enums;

namespace MovieApp.Requests.Order;

public class OrderCreationRequest
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter<PaymentMethod>))]
    public PaymentMethod PaymentMethod { get; set; }


    [Required]
    public List<OrderItemRequest> OrderItems { get; set; } = [];
}