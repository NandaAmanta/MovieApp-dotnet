

using System.ComponentModel.DataAnnotations;

namespace MovieApp.Requests.Studio;

public class StudioUpdateRequest
{

    [Required]
    public required string StudioNumber { get; set; }

    [Required]
    public required int SeatCapacity { get; set; }

}