

using FluentValidation;
using MovieApp.Models;

namespace MovieApp.Requests.Tags;

public class TagCreationRequest
{
    public string? Name { get; set; }
}