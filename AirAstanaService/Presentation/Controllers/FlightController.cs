using AirAstanaService.Application.Commands;
using AirAstanaService.Application.Queries;
using AirAstanaService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirAstanaService.Presentation.Controllers;

/// <summary>
/// Контроллер для управления рейсами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FlightController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FlightController> _logger;

    public FlightController(IMediator mediator, ILogger<FlightController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Получить список рейсов
    /// </summary>
    /// <param name="origin">Место отправления</param>
    /// <param name="destination">Место прибытия</param>
    /// <returns>Список рейсов</returns>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Flight>>> GetFlights([FromQuery] string origin,
        [FromQuery] string destination)
    {
        _logger.LogInformation("Fetching flights with origin: {Origin} and destination: {Destination}", origin,
            destination);
        var query = new GetFlightsQuery { Origin = origin, Destination = destination };
        var flights = await _mediator.Send(query);
        return Ok(flights);
    }

    /// <summary>
    /// Добавить новый рейс
    /// </summary>
    /// <param name="flight">Данные рейса</param>
    /// <returns>Созданный рейс</returns>
    [HttpPost]
    [Authorize(Roles = "Moderator")]
    public async Task<ActionResult<Flight>> PostFlight([FromBody] AddFlightCommand command)
    {
        var flight = await _mediator.Send(command);
        _logger.LogInformation("Added new flight: {@Flight}", flight);
        return CreatedAtAction(nameof(GetFlights), new { id = flight.ID }, flight);
    }

    /// <summary>
    /// Обновить существующий рейс
    /// </summary>
    /// <param name="id">ID рейса</param>
    /// <param name="flight">Обновленные данные рейса</param>
    [HttpPut("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> PutFlight(int id, [FromBody] Flight flight)
    {
        if (id != flight.ID)
        {
            return BadRequest();
        }

        await _mediator.Send(new UpdateFlightCommand { Flight = flight });
        _logger.LogInformation("Updated flight: {@Flight}", flight);
        return NoContent();
    }

    /// <summary>
    /// Удалить рейс
    /// </summary>
    /// <param name="id">ID рейса</param>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task<IActionResult> DeleteFlight(int id)
    {
        await _mediator.Send(new DeleteFlightCommand { Id = id });
        _logger.LogInformation("Deleted flight with ID: {Id}", id);
        return NoContent();
    }
} 