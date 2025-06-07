using System.ComponentModel.DataAnnotations;

namespace TripManagement.Models;

public class ClientTrip
{
    [Required]
    public int IdClient { get; set; }
        
    [Required]
    public int IdTrip { get; set; }
        
    [Required]
    public DateTime RegisteredAt { get; set; }
        
    public DateTime? PaymentDate { get; set; }

    public virtual Client Client { get; set; } = null!;
    public virtual Trip Trip { get; set; } = null!;
}