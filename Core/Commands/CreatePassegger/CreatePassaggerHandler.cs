using SimpleSoft.Mediator;
using Dominio;
using Dominio.Validation;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Routing;
using Core.Commands.CreateTicket;
using Dominio.Passegger;
using FluentResults;
using SimpleSoft.Mediator;
namespace Core.Commands.CreatePassegger
{
    static class MethodToCreatePassegger
    {
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
        private readonly IRepositoryPassegger _repository;
        private readonly Mediator _mediator;
        public CreatePassaggerHandler(IRepositoryPassegger repository, Mediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }
        public async Task<Result<PasseggerResult>> HandleAsync(CreatePasseggerCommand cmd, CancellationToken ct)
        {
            var newPassegger = new Passegger();
            newPassegger.SetPersonalInfomation(cmd.Nome, cmd.Cognome, cmd.Etá)
            .AddLuggage(cmd.Luggages);

            if (newPassegger.IsValid())
            {
                var cmdTicket = ParsePasseggerCommandToTicketCommand(cmd,newPassegger.Id);
                var ResultTicket = _mediator.Send(cmdTicket);
                if (ResultTicket.IsSuccess)
                    return await LinkPasseggerToTicket(newPassegger, ResultTicket.Value.Id);
                return Result.Fail("Impossibile creare il ticket");
            }
            return Result.Fail("Impossibile creare il passeggero");

        }
        private TicketCommand ParsePasseggerCommandToTicketCommand(CreatePasseggerCommand cmd,string IdPassegger)
        {
            var cmdTicket = new TicketCommand
            {
                IdFlightRoute = cmd.IdRoute,
                TypeTicket = cmd.TypeTicket,
                IdPassegger = IdPassegger
            };
            return cmdTicket;
        }


        private async Task<Result<PasseggerResult>> LinkPasseggerToTicket(Passegger Passegger, string IdTicket)
        {
                Passegger.IdTicket = IdTicket;
                var ResultRoute = await _repository.AddNewPasseggerWithTicket(IdTicket, Passegger);     
                {
                    if (ResultRoute)
                        return Result.Ok(new PasseggerResult(Passegger.Id));
                }
                return Result.Fail("Impossibile inserire il passeggero");  
        }
    }
}
