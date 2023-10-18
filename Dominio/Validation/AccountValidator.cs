using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Passegger;
namespace Dominio.Validation
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator() {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Name).Length(3, 10);
            RuleFor(x=>x.Cognome).NotEmpty().NotNull(); 
            RuleFor(x=> x.Cognome).Length(3,15);       
            RuleFor(x => x.Etá).GreaterThan(0);
        }

    }
}
