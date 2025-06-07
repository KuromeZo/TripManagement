namespace TripManagement.Services;

public interface IClientService
{
    Task<bool> DeleteClientAsync(int idClient);
}