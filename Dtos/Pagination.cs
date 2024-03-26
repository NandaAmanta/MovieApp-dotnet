

using System.Text.Json.Serialization;

namespace MovieApp.Dtos;

public class Pagination<T>(List<T> items, int pageIndex, int totalPages, int totalItems)
{
    public List<T> Items { get; } = items;

    [JsonPropertyName("page")]
    public int Page { get; } = pageIndex;

    [JsonPropertyName("total_pages")]

    public int TotalPages { get; } = totalPages;

    [JsonPropertyName("total_items")]
    public int TotalItems { get; } = totalItems;
}