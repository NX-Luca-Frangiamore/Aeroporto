using SimpleSoft.Mediator;
using Dominio.Validation;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Routing;

using Dominio.Passegger;
using FluentResults;
namespace Core.Commands.CreatePassegger
{

    internal class CreateTicketHandler : ICommandHandler<CreateTicketCommand, Result<TicketResult>>
    {
        private readonly IRepository _repository;
        public CreateTicketHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<TicketResult>> HandleAsync(CreateTicketCommand cmd, CancellationToken ct)
        {
            var TicketResult = await CreateTicket(cmd.IdRoute, cmd.TypeTicket.ToString());
            if (TicketResult.IsFailed)
                return Result.Fail(TicketResult.Errors);
            var Ticket = TicketResult.Value;

            AddLuggage(ref Ticket, cmd.Luggages);

            if (Ticket.IsValid())
            {
                var ResultRoute = await _repository.AddNewTicketToAccount(cmd.IdAccount, Ticket);
                {
                    if (ResultRoute)
                        return Result.Ok(new TicketResult(Ticket.Id));
                }
            }
            return Result.Fail("Impossibile creare il ticket");

        }
        private static void AddLuggage(ref Ticket p, List<CreateLuggageCommand>? luggages)
        {
            if (luggages is null) return;

            foreach(var l in luggages)
            {
                var luggage = new Luggage
                {
                    Peso = l.Peso,
                    Dimensione = l.Dimensione,
                };
                p.Luggages.Add(luggage);
            };
        }
        private async Task<Result<Ticket>> CreateTicket(string idRoute, string typeTicket)
        {
            var Route = await _repository.GetRoute(idRoute);
            if (Route.Value.NSeatsLeft > 0)
            {
                var NewTicket = new Ticket()
                {
                    TycketClassTicket = typeTicket,
                    Seat = Route.Value.NSeatsLeft
                };
                if (new TicketValidator().Validate(NewTicket).IsValid)
                {
                    return Result.Ok(NewTicket);
                }
                return Result.Fail("Impossibile creare un nuovo ticket");
            }
            return Result.Fail("Posti esauriti");
        }
    }
}
