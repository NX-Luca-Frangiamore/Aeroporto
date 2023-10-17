using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Passegger;
namespace Dominio.Validation
{
    public class TicketValidator:AbstractValidator<Ticket>
    {
        public TicketValidator() {
           RuleFor(x=>x.Id).NotEmpty();
           RuleFor(x => x.Seat).Must(p => p > 0);
        }

    }
}
