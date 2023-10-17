using Dominio;
using FluentResults;
using Dominio.Passegger;
namespace Core
{
    public interface IRepository
    {
        Task<bool> GetPassegger(string idPassegger);
        Task<bool> AddNewPassegger(Passegger passegger);
        Task<bool> DeletePasseggerToRoute(string idPassegger);
        Task<bool> ChangePasseggerToRoute(string idPassegger, Passegger changedPassegger);
        Task<Result<FlightRoute>> GetRoute(string idRoute);
        Task<bool> NewRoute(Aereo aereo, FlightRoute route);
        Task<bool> DeleteRoute(string idRoute);
        Task<bool> ChangeRoute(string idRoute, FlightRoute changedRoute);
        Task<bool> NewTicketToRoute(string idRoute, Ticket ticket);
        Task<bool> DeleteTicketToRoute(string idTicket);

    }

}
