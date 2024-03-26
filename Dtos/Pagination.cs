

namespace MovieApp.Dtos;

public class Pagination<T>(List<T> items, int pageIndex, int totalPages, int totalItems)
{
    public List<T> Items { get; } = items;
    public int PageIndex { get; } = pageIndex;
    public int TotalPages { get; } = totalPages;
    public int TotalItems { get; } = totalItems;
}