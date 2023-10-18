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
        public Guid Id { get; set; }= Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Ticket> Tickets { get; set; } = new();
        public bool IsValid()
        {
            var v = new AccountValidator().Validate(this);
            return v.IsValid;
        }
    }
}
