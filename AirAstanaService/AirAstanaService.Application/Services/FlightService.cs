using AirAstanaService.Application.DTOs;
using AirAstanaService.Application.Interfaces;
using AirAstanaService.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AirAstanaService.Application.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IDistributedCache _cache;
    private readonly ILogger<FlightService> _logger;
    private const string CacheKey = "flights";

    public FlightService(IFlightRepository flightRepository, IDistributedCache cache, ILogger<FlightService> logger)
    {
        _flightRepository = flightRepository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<FlightDTO> AddFlightAsync(FlightDTO flightDto)
    {
        var flight = new Flight
        {
            Origin = flightDto.Origin,
            Destination = flightDto.Destination,
            Departure = flightDto.Departure,
            Arrival = flightDto.Arrival,
            Status = Enum.Parse<FlightStatus>(flightDto.Status)
        };

        await _flightRepository.AddFlightAsync(flight);
        flightDto.ID = flight.ID;

        await UpdateCache();

        return flightDto;
    }

    public async Task UpdateFlightAsync(FlightDTO flightDto)
    {
        var flight = new Flight
        {
            ID = flightDto.ID,
            Origin = flightDto.Origin,
            Destination = flightDto.Destination,
            Departure = flightDto.Departure,
            Arrival = flightDto.Arrival,
            Status = Enum.Parse<FlightStatus>(flightDto.Status)
        };

        await _flightRepository.UpdateFlightAsync(flight);
        await UpdateCache();
    }

    public async Task DeleteFlightAsync(int id)
    {
        await _flightRepository.DeleteFlightAsync(id);
        await UpdateCache();
    }

    public async Task<IEnumerable<FlightDTO>> GetFlightsAsync(string origin, string destination)
    {
        string cacheKey = $"{CacheKey}_{origin}_{destination}";
        var cachedFlights = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedFlights))
        {
            _logger.LogInformation("Получено из кэша: {CacheKey}", cacheKey);
            return JsonConvert.DeserializeObject<IEnumerable<FlightDTO>>(cachedFlights);
        }

        var flights = await _flightRepository.GetFlightsAsync(origin, destination);
        var flightDtos = flights.Select(f => new FlightDTO
        {
            ID = f.ID,
            Origin = f.Origin,
            Destination = f.Destination,
            Departure = f.Departure,
            Arrival = f.Arrival,
            Status = f.Status.ToString()
        }).ToList();

        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(flightDtos), cacheOptions);
        _logger.LogInformation("Добавлено в кэш: {CacheKey}", cacheKey);

        return flightDtos;
    }


    private async Task UpdateCache()
    {
        var flights = await _flightRepository.GetFlightsAsync(null, null);
        var flightDtos = flights.Select(f => new FlightDTO
        {
            ID = f.ID,
            Origin = f.Origin,
            Destination = f.Destination,
            Departure = f.Departure,
            Arrival = f.Arrival,
            Status = f.Status.ToString()
        });

        await _cache.SetStringAsync(CacheKey, JsonConvert.SerializeObject(flightDtos), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });
    }
}
