using Dominio;
using FluentResults;
using Dominio.Passegger;
namespace Core
{
    public interface IRepository
    {
        Task<bool> NewAccout(Account account);
        Task<bool> ChangeAccout(Account account);

        Task<bool> GetTicket(string idTicket);
        Task<bool> AddNewTicketToAccount(string IdAccout,Ticket passegger);
        Task<bool> DeleteTicketToRoute(string idPassegger);
        Task<bool> ChangeTicketToRoute(string idPassegger, Ticket changedPassegger);

        Task<Result<FlightRoute>> GetRoute(string idRoute);
        Task<bool> NewRoute(Aereo aereo, FlightRoute route);
        Task<bool> DeleteRoute(string idRoute);
        Task<bool> ChangeRoute(string idRoute, FlightRoute changedRoute);

    }

}
