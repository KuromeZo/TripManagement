using Microsoft.AspNetCore.Mvc;
using TripManagement.Services;

namespace TripManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        try
        {
            var result = await _clientService.DeleteClientAsync(idClient);
                
            if (!result)
            {
                return NotFound(new { error = "Client not found" });
            }

            return Ok(new { message = "Client successfully deleted" });
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