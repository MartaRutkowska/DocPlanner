using DocPlanner.Models.Request;
using FluentValidation;

namespace DocPlanner.Validators
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.FacilityId).NotEmpty();
            RuleFor(x => x.Start).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.End).GreaterThan(x => x.Start);
            RuleFor(x => x.Patient).SetValidator(new PatientValidator());
        }
    }
}
