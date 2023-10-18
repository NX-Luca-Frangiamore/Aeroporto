using Dominio;
using FluentResults;
using Dominio.Passegger;
namespace Core
{
    public interface IRepository
    {
        Task<Account?> GetAccout(Account account);
        Task<bool> NewAccout(Account account);
        Task<bool> ChangeAccout(Account account);

        Task<Ticket?> GetTicket(Guid idTicket);
        Task<bool> AddNewTicketToAccount(Guid IdAccout,Ticket passegger);
        Task<bool> DeleteTicketToRoute(string idPassegger);
        Task<bool> ChangeTicketToRoute(string idPassegger, Ticket changedPassegger);

        Task<FlightRoute?> GetRoute(Guid idRoute);
        Task<bool> NewRoute(Aereo aereo, FlightRoute route);
        Task<bool> DeleteRoute(Guid idRoute);
        Task<bool> ChangeRoute(Guid idRoute, FlightRoute changedRoute);

    }

}
