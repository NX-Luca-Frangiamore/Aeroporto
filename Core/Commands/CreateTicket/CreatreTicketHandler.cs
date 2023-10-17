using Dominio;
using Dominio.Validation;
using FluentResults;
using Microsoft.AspNetCore.Routing;
using SimpleSoft.Mediator;

namespace Core.Commands.CreateTicket
{
    public class CreatreTicketHandler:ICommandHandler<TicketCommand,Result<TicketResult>>
    {
        private readonly IRepositoryTicket _repositoryTicket;
        private readonly IRepositoryRoute _repositoryRoute;
        public CreatreTicketHandler(IRepositoryTicket repositoryTicket, IRepositoryRoute repositoryRoute) {
            _repositoryTicket = repositoryTicket; 
            _repositoryRoute= repositoryRoute;
        }

        public async Task<Result<TicketResult>> HandleAsync(TicketCommand cmd, CancellationToken ct)
        {
            var Route= await _repositoryRoute.GetRoute(cmd.IdFlightRoute);
            if (Route.Value.NSeatsLeft>0)
            {
                var NewTicket = new Ticket()
                {
                    TycketClassTicket = cmd.TypeTicket.ToString(),
                    Seat = Route.Value.NSeatsLeft
                };
                if (new TicketValidator().Validate(NewTicket).IsValid)
                {
                    var ResultOfNewTicket = await  _repositoryTicket.NewTicketToRoute(cmd.IdFlightRoute, NewTicket);
                    if (ResultOfNewTicket)
                        return Result.Ok(new TicketResult() { Id = NewTicket.Id });
                }
                return Result.Fail("Impossibile creare un nuovo ticket");
            }
            return Result.Fail("Posti esauriti");
        }
    }
}
