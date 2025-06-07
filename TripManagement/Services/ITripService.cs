using TripManagement.DTOs;

namespace TripManagement.Services;

public interface ITripService
{
    Task<TripsResponseDto> GetTripsAsync(int page, int pageSize);
    Task<bool> AssignClientToTripAsync(int idTrip, AssignClientDto clientDto);
}