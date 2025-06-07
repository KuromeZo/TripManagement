using Microsoft.EntityFrameworkCore;
using TripManagement.Data;

namespace TripManagement.Services;

public class ClientService : IClientService
{
    private readonly TripDbContext _context;

    public ClientService(TripDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteClientAsync(int idClient)
    {
        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client == null)
        {
            return false;
        }

        // if client has any trips
        if (client.ClientTrips.Any())
        {
            throw new InvalidOperationException("Cannot delete client with assigned trips");
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return true;
    }
}