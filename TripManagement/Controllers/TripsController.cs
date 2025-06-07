using Microsoft.AspNetCore.Mvc;
using TripManagement.DTOs;
using TripManagement.Services;

namespace TripManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<ActionResult<TripsResponseDto>> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var result = await _tripService.GetTripsAsync(page, pageSize);
        return Ok(result);
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientDto clientDto)
    {
        try
        {
            await _tripService.AssignClientToTripAsync(idTrip, clientDto);
            return Ok(new { message = "Client successfully assigned to trip" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "An error occurred while processing the request" });
        }
    }
}