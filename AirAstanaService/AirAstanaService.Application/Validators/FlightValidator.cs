using AirAstanaService.Application.DTOs;
using AirAstanaService.Domain.Entities;
using FluentValidation;

namespace AirAstanaService.Application.Validators;

public class FlightDTOValidator : AbstractValidator<FlightDTO>
{
    public FlightDTOValidator()
    {
        RuleFor(f => f.Origin).NotEmpty().WithMessage("Origin is required.")
            .MaximumLength(256).WithMessage("Origin must be less than 256 characters.");
        RuleFor(f => f.Destination).NotEmpty().WithMessage("Destination is required.")
            .MaximumLength(256).WithMessage("Destination must be less than 256 characters.");
        RuleFor(f => f.Departure).NotEmpty().WithMessage("Departure time is required.");
        RuleFor(f => f.Arrival).NotEmpty().WithMessage("Arrival time is required.");
        RuleFor(f => f.Status).Must(BeAValidStatus).WithMessage("Invalid status.");
    }

    private bool BeAValidStatus(string status)
    {
        return Enum.TryParse(typeof(FlightStatus), status, true, out _);
    }
}