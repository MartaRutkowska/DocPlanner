using FluentValidation;

namespace DocPlanner.Validators
{
    public class DateValidator : AbstractValidator<DateTime>
    {
        public DateValidator()
        {
            RuleFor(date => date).GreaterThanOrEqualTo(DateTime.Now.Date);
        }
    }
}
