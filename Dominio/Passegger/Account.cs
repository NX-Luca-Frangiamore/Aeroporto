using Dominio.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Passegger
{
    public class Account:Person
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Ticket> Tickets { get; set; }
        public bool IsValid()
        {
            var v = new AccountValidator().Validate(this);
            return v.IsValid;
        }
    }
}
