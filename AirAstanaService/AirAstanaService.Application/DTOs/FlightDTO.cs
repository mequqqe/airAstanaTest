namespace AirAstanaService.Application.DTOs;

public class FlightDTO
{
    public int ID { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTimeOffset Departure { get; set; }
    public DateTimeOffset Arrival { get; set; }
    public string Status { get; set; }
}