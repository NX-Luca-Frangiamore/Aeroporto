using Core.Commands.CreatePassegger;
using Dominio;
using FluentResults;
using SimpleSoft.Mediator;

namespace Core.Commands.CreateTicket
{
    public class TicketCommand:Command<Result<TicketResult>>
    {
        public TypeClassTicket TypeTicket { get; set; }
        public string IdFlightRoute { get; set; }

        public string IdPassegger {  get; set; }
    }
    public enum TypeClassTicket
    {
        Priority, NonPriority
    }
}
