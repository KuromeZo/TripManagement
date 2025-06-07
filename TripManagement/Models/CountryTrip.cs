using System.ComponentModel.DataAnnotations;

namespace TripManagement.Models;

public class CountryTrip
{
    [Required]
    public int IdCountry { get; set; }
        
    [Required]
    public int IdTrip { get; set; }

    public virtual Country Country { get; set; } = null!;
    public virtual Trip Trip { get; set; } = null!;
}