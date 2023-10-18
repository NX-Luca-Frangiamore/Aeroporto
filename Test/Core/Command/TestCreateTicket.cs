using Moq;
using Core;
using Dominio;
using FluentResults;
using FluentAssertions;
using Core.Commands.CreatePassegger;
using Dominio.Passegger;

namespace Test.Core.Command
{    
    public class TestCreateTicket
    {
        readonly Guid GuidRotta = Guid.NewGuid();
        readonly Guid GuidAccout = Guid.NewGuid();
        [Fact]
        public async void GetCorrectTicket_WithCorrectCommand()
        {
            //Arrange
            var MockFlightRoute = new Mock<FlightRoute>().Object;
            MockFlightRoute.NSeatsLeft = 9;
            MockFlightRoute.Id = GuidRotta;
            var FakeFlightRoute = Task.FromResult(MockFlightRoute);

            var MockRepository = new Mock<IRepository>();
            MockRepository.Setup(x => x.GetRoute(GuidRotta)).Returns(FakeFlightRoute!);
            MockRepository.Setup(x => x.AddNewTicketToAccount(GuidAccout,It.IsAny<Ticket>())).Returns(Task.FromResult(true));

            //Act
            var Handler =new CreateTicketHandler(MockRepository.Object);

            var command = new CreateTicketCommand()
            {
                IdRoute = GuidRotta,
                TypeTicket = "First",
                IdAccount =GuidAccout,
                Luggages= new List<CreateLuggageCommand> {new CreateLuggageCommand() { Dimensione=9,Peso=10} }
            };
            var ResultTicket=await Handler.HandleAsync(command,CancellationToken.None);
            
            //Assert
            ResultTicket.IsSuccess.Should().BeTrue();
            ResultTicket.Value.Seat.Should().Be(9);
        }

        [Fact]
        public async void GetError_WithNoExistingRoute()
        {
            //Arrange
            var FakeFlightRoute = Task.FromResult<FlightRoute>(null!);

            var MockRepository = new Mock<IRepository>();
            MockRepository.Setup(x => x.GetRoute(GuidRotta)).Returns(FakeFlightRoute!);
            MockRepository.Setup(x => x.AddNewTicketToAccount(GuidAccout, It.IsAny<Ticket>())).Returns(Task.FromResult(true));

            //Act
            var Handler = new CreateTicketHandler(MockRepository.Object);

            var command = new CreateTicketCommand()
            {
                IdRoute = GuidRotta,
                TypeTicket = "First",
                IdAccount = GuidAccout,
                Luggages = new List<CreateLuggageCommand> { new CreateLuggageCommand() { Dimensione = 9, Peso = 10 } }
            };
            var ResultTicket = await Handler.HandleAsync(command, CancellationToken.None);

            //Assert
            ResultTicket.IsSuccess.Should().BeFalse();
        }
    }
}
