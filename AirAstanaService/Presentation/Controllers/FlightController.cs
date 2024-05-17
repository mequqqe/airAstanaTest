using AirAstanaService.Application.Interfaces;
using AirAstanaService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirAstanaService.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightService;
    private readonly ILogger<FlightController> _logger;

    public FlightController(IFlightService flightService, ILogger<FlightController> logger)
    {
        _flightService = flightService;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Flight>>> GetFlights([FromQuery] string origin, [FromQuery] string destination)
    {
        _logger.LogInformation("Fetching flights with origin: {Origin} and destination: {Destination}", origin, destination);
        var flights = await _flightService.GetFlightsAsync(origin, destination);
        return Ok(flights);
    }

    [HttpPost]
    [Authorize(Roles = "Moderator")]
    public async Task<ActionResult<Flight>> PostFlight([FromBody] Flight flight)
    {
        await _flightService.AddFlightAsync(flight);
        _logger.LogInformation("Added new flight: {@Flight}", flight);
        return CreatedAtAction(nameof(GetFlights), new { id = flight.ID }, flight);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> PutFlight(int id, [FromBody] Flight flight)
    {
        if (id != flight.ID)
        {
            return BadRequest();
        }

        await _flightService.UpdateFlightAsync(flight);
        _logger.LogInformation("Updated flight: {@Flight}", flight);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> DeleteFlight(int id)
    {
        await _flightService.DeleteFlightAsync(id);
        _logger.LogInformation("Deleted flight with ID: {Id}", id);
        return NoContent();
    }
}