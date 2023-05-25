using FluentValidation;

namespace DemoApp.Domain.Models.Validators
{
    public sealed class TimesheetValidator : AbstractValidator<Timesheet>
    {
        public TimesheetValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
