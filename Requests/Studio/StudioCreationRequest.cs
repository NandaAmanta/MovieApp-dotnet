

using System.ComponentModel.DataAnnotations;

namespace MovieApp.Requests.Studio;

public class StudioCreationRequest
{

    [Required]
    public required string StudioNumber { get; set; }

    [Required]
    public required int SeatCapacity { get; set; }

}