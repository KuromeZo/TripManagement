using System.ComponentModel.DataAnnotations;

namespace TripManagement.Models;

public class Trip
{
    public int IdTrip { get; set; }
        
    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;
        
    [Required]
    [MaxLength(220)]
    public string Description { get; set; } = string.Empty;
        
    [Required]
    public DateTime DateFrom { get; set; }
        
    [Required]
    public DateTime DateTo { get; set; }
        
    [Required]
    public int MaxPeople { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
    public virtual ICollection<CountryTrip> CountryTrips { get; set; } = new List<CountryTrip>();
}