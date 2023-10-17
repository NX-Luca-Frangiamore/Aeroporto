using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Passegger;
namespace Dominio.Validation
{
    public class PasseggerValidator:AbstractValidator<Passegger.Passegger>
    {
        public PasseggerValidator() {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Name).Length(3, 10);
            RuleFor(x=>x.Cognome).NotEmpty().NotNull(); 
            RuleFor(x=> x.Cognome).Length(3,15);
            RuleForEach(x => x.Luggages).Must(p => p.Peso > 0).Must(d => d.Dimensione > 0);
            RuleFor(x => x.Etá).GreaterThan(0);
        }

    }
}
