using FluentValidation;
using FreeBilling.Web.Models;

namespace FreeBilling.Web.Validators
{
    public class TimeBillModelValidator:AbstractValidator<TimeBillModel>
    {
        public TimeBillModelValidator()
        {
            RuleFor(_ => _.CustomerId).NotEmpty();
            RuleFor(_ => _.EmployeeId).NotEmpty();
            RuleFor(_ => _.BillingRate).InclusiveBetween(50.0, 300.0);
            RuleFor(_ => _.Hours).InclusiveBetween(.1, 12.0);
            RuleFor(_ => _.WorkPerformed).NotEmpty().MinimumLength(15);
        }
    }
}
