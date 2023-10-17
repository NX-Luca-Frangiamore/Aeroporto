using Core.Commands.CreateTicket;
using FluentResults;
using SimpleSoft.Mediator;

namespace Core.Commands.CreatePassegger
{
    internal class CreatePasseggerCommand : Command<Result<PasseggerResult>>
    {
        public string IdRoute { get;init; }
        public required string Nome { get; init; }
        public required string Cognome { get; init; }
        public List<CreateLuggageCommand>? Luggages { get; init; }
        public TypeClassTicket TypeTicket { get; init; }
        public int Etá { get; set; }
    }

    class CreateLuggageCommand
    {
        public float Peso { get; init; }
        public float Dimensione { get; init; }
    }
   
}
