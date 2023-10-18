
using FluentResults;
using SimpleSoft.Mediator;

namespace Core.Commands.CreatePassegger
{
    public class CreateTicketCommand : Command<Result<CreateTicketResult>>
    {
        public required Guid IdRoute { get;init; }
        public List<CreateLuggageCommand>? Luggages { get; init; }
        public required string TypeTicket { get; init; }
        public required Guid IdAccount {  get; init; } 
    }

    public class CreateLuggageCommand
    {
        public required float Peso { get; init; }
        public required float Dimensione { get; init; }
    }
   
}
