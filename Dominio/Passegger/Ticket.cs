using Dominio.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Passegger
{

    public class Ticket
    {
        public string Id { get; set; }
        public List<Luggage> Luggages { get; set; }
        public int Seat { get; set; }
        public string TycketClassTicket { get; set; }

        public bool IsValid()
        {
            var v = new TicketValidator().Validate(this);
            return v.IsValid;
        }
    }
}
