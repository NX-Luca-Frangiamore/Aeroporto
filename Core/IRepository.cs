using Dominio;
using FluentResults;
using Dominio.Passegger;
namespace Core
{
    public interface IRepositoryPassegger
    {
        Task<bool> GetPassegger(string idPassegger);
        Task<bool> AddNewPasseggerWithTicket(string idTicket, Passegger person);
        Task<bool> DeletePasseggerToRoute(string idPassegger);
        Task<bool> ChangePasseggerToRoute(string idPassegger, Passegger ChangedPassegger);
    }
    public interface IRepositoryRoute
    {
        Task<Result<FlightRoute>> GetRoute(string idRoute);
        Task<bool> NewRoute(Aereo aereo, FlightRoute Route);
        Task<bool> DeleteRoute(string idRoute);
        Task<bool> ChangeRoute(string idRoute, FlightRoute ChangedRoute);
    }
    public interface IRepositoryTicket
    {    
         Task<bool> NewTicketToRoute(string idRoute,Ticket ticket);
         Task<bool> DeleteTicketToRoute(string idTicket);
    
    }
}
