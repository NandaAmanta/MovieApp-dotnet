

using System.Net;

namespace MovieApp.Utils;

public class ApiResponser<T>(HttpStatusCode status, string message, T? data)
{
    public int StatusCode { get; set; } = (int)status;
    public string Message { get; set; } = message;

    public T? data { get; set; } = data;
}