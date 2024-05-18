using AirAstanaService.Application.Commands;
using AirAstanaService.Application.Interfaces;
using MediatR;

namespace AirAstanaService.Application.Handlers;

public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand, Unit>
{
    private readonly IFlightRepository _flightRepository;

    public UpdateFlightCommandHandler(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<Unit> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
    {
        await _flightRepository.UpdateFlightAsync(request.Flight);
        return Unit.Value;
    }
}