
using FluentResults;
using SimpleSoft.Mediator;

namespace Core.Commands.CreatePassegger
{
    public enum TycketClass { First, Second }
    internal class CreateTicketCommand : Command<Result<TicketResult>>
    {
        public required string IdRoute { get;init; }
        public List<CreateLuggageCommand>? Luggages { get; init; }
        public required TycketClass TypeTicket { get; init; }
        public required string IdAccount {  get; init; } 
    }

    class CreateLuggageCommand
    {
        public required float Peso { get; init; }
        public required float Dimensione { get; init; }
    }
   
}
