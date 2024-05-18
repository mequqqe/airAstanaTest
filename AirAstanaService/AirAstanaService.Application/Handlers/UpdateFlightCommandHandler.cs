using AirAstanaService.Application.Commands;
using AirAstanaService.Application.Interfaces;
using MediatR;

namespace AirAstanaService.Application.Handlers;

public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand, Unit>
{
    private readonly IFlightService _flightService;

    public UpdateFlightCommandHandler(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public async Task<Unit> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
    {
        await _flightService.UpdateFlightAsync(request.Flight);
        return Unit.Value;
    }
}
