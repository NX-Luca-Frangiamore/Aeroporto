
using FluentResults;
using SimpleSoft.Mediator;

namespace Core.Commands.CreatePassegger
{
    public enum TycketClass { First, Second }
    internal class CreateTicketCommand : Command<Result<TicketResult>>
    {
        public string IdRoute { get;init; }
        public List<CreateLuggageCommand>? Luggages { get; init; }
        public TycketClass TypeTicket { get; init; }
        public string IdAccount {  get; init; } 
    }

    class CreateLuggageCommand
    {
        public float Peso { get; init; }
        public float Dimensione { get; init; }
    }
   
}
