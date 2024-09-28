using DocPlanner.Models.Request;
using FluentValidation;

namespace DocPlanner.Validators
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.SecondName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).NotEmpty();
        }
    }
}
