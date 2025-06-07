using System.ComponentModel.DataAnnotations;

namespace TripManagement.Models;

public class Country
{
    public int IdCountry { get; set; }
        
    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<CountryTrip> CountryTrips { get; set; } = new List<CountryTrip>();
}