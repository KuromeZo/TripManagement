using System.ComponentModel.DataAnnotations;

namespace TripManagement.Models;

public class Client
{
    public int IdClient { get; set; }
        
    [Required]
    [MaxLength(120)]
    public string FirstName { get; set; } = string.Empty;
        
    [Required]
    [MaxLength(120)]
    public string LastName { get; set; } = string.Empty;
        
    [Required]
    [MaxLength(120)]
    public string Email { get; set; } = string.Empty;
        
    [Required]
    [MaxLength(120)]
    public string Telephone { get; set; } = string.Empty;
        
    [Required]
    [MaxLength(120)]
    public string Pesel { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
}