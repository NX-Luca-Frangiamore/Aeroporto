using SimpleSoft.Mediator;
using Dominio;
using Dominio.Validation;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Routing;

using Dominio.Passegger;
using FluentResults;
namespace Core.Commands.CreatePassegger
{
    static class MethodToCreatePassegger
    {
        internal static Passegger SetTicket(this Passegger p, Ticket ticket)
        {
            p.Ticket = ticket;
            return p;
        }
        internal static Passegger SetPersonalInfomation(this Passegger p, string nome, string cognome, int Etá)
        {
            p.Name = nome;
            p.Cognome = cognome;
            p.Etá = Etá;
            return p;
        }
        internal static Passegger AddLuggage(this Passegger p, List<CreateLuggageCommand>? luggages)
        {
            if (luggages is null) return p;

            luggages.ForEach(l =>
            {
                var luggage = new Luggage
                {
                    Peso = l.Peso,
                    Dimensione = l.Dimensione,
                };
                p.Luggages.Add(luggage);

            });

            return p;
        }  
    }
    internal class CreatePassaggerHandler : ICommandHandler<CreatePasseggerCommand, Result<PasseggerResult>>
    {
        private readonly IRepository _repository;
        public CreatePassaggerHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<PasseggerResult>> HandleAsync(CreatePasseggerCommand cmd, CancellationToken ct)
        {
            var Ticket = await CreateTicket(cmd.IdRoute, cmd.TypeTicket.ToString());
            if (Ticket.IsFailed) 
                return Result.Fail(Ticket.Errors);

            var newPassegger = new Passegger()
            .SetPersonalInfomation(cmd.Nome, cmd.Cognome, cmd.Etá)
            .AddLuggage(cmd.Luggages)
            .SetTicket(Ticket.Value);

            if (newPassegger.IsValid())
            {
                var ResultRoute = await _repository.AddNewPassegger(newPassegger);
                {
                    if (ResultRoute)
                        return Result.Ok(new PasseggerResult(newPassegger.Id));
                }
            }
            return Result.Fail("Impossibile creare il passeggero");

        }
        private async Task<Result<Ticket>> CreateTicket(string idRoute,string typeTicket) 
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
