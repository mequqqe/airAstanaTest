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

        public async Task<IEnumerable<Flight>> GetFlightsAsync(string origin, string destination)
        {
            _logger.LogInformation("Fetching flights from cache.");
            var cachedFlights = await _cache.GetStringAsync(CacheKey);

            if (!string.IsNullOrEmpty(cachedFlights))
            {
                _logger.LogInformation("Flights found in cache.");
                var flights = JsonConvert.DeserializeObject<IEnumerable<Flight>>(cachedFlights);
                if (!string.IsNullOrEmpty(origin) || !string.IsNullOrEmpty(destination))
                {
                    flights = flights.Where(f =>
                        (string.IsNullOrEmpty(origin) || f.Origin == origin) &&
                        (string.IsNullOrEmpty(destination) || f.Destination == destination));
                }
                return flights;
            }

            _logger.LogInformation("Flights not found in cache. Fetching from database.");
            var flightList = await _flightRepository.GetFlightsAsync(origin, destination);
            var serializedFlights = JsonConvert.SerializeObject(flightList);
            await _cache.SetStringAsync(CacheKey, serializedFlights);

            return flightList;
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            _logger.LogInformation("Fetching flight by ID from cache.");
            var flights = await GetFlightsAsync(null, null);
            return flights?.FirstOrDefault(f => f.ID == id);
        }

        public async Task AddFlightAsync(Flight flight)
        {
            _logger.LogInformation("Adding flight to database.");
            await _flightRepository.AddFlightAsync(flight);
            await UpdateCache();
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            _logger.LogInformation("Updating flight in database.");
            await _flightRepository.UpdateFlightAsync(flight);
            await UpdateCache();
        }

        public async Task DeleteFlightAsync(int id)
        {
            _logger.LogInformation("Deleting flight from database.");
            await _flightRepository.DeleteFlightAsync(id);
            await UpdateCache();
        }

        private async Task UpdateCache()
        {
            _logger.LogInformation("Updating cache.");
            var flights = await _flightRepository.GetFlightsAsync(null, null);
            var serializedFlights = JsonConvert.SerializeObject(flights);
            await _cache.SetStringAsync(CacheKey, serializedFlights);
        }
    }