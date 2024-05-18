using System.Collections.Generic;
using System.Threading.Tasks;
using AirAstanaService.Application.Commands;
using AirAstanaService.Application.Queries;
using AirAstanaService.Domain.Entities;
using AirAstanaService.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AirAstanaService.Tests
{
    public class FlightControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<ILogger<FlightController>> _loggerMock;
        private FlightController _controller;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<FlightController>>();
            _controller = new FlightController(_mediatorMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetFlights_ShouldReturnOkResult_WithListOfFlights()
        {
            var flights = new List<Flight>
            {
                new Flight { ID = 1, Origin = "Origin1", Destination = "Destination1" }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetFlightsQuery>(), default)).ReturnsAsync(flights);

            var result = await _controller.GetFlights("Origin1", "Destination1");

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnValue = okResult.Value as List<Flight>;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(1, returnValue.Count);
        }

        [Test]
        public async Task PostFlight_ShouldReturnCreatedAtActionResult()
        {
            var command = new AddFlightCommand { Origin = "Origin1", Destination = "Destination1" };
            var flight = new Flight { ID = 1, Origin = "Origin1", Destination = "Destination1" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddFlightCommand>(), default)).ReturnsAsync(flight);
            
            var result = await _controller.PostFlight(command);
            
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            var returnValue = createdAtActionResult.Value as Flight;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(flight.ID, returnValue.ID);
        }

        [Test]
        public async Task PutFlight_ShouldReturnNoContentResult()
        {
            var flight = new Flight { ID = 1, Origin = "Origin1", Destination = "Destination1" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateFlightCommand>(), default)).ReturnsAsync(Unit.Value);
            
            var result = await _controller.PutFlight(1, flight);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteFlight_ShouldReturnNoContentResult()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteFlightCommand>(), default)).ReturnsAsync(Unit.Value);
            
            var result = await _controller.DeleteFlight(1);
            
            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}
