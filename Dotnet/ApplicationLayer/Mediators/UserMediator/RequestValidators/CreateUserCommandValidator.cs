using DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Commands;
using FluentValidation;
using System.Globalization;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.RequestValidators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(cmd => cmd.UserToAdd.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Name field is null")
                .Length(3,10).WithMessage("Name length must be between [3,10]")
                .NotEmpty().WithMessage("Name field is empty");
            RuleFor(cmd => cmd.UserToAdd.Surname)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Surname field is null")
                .Length(3, 10).WithMessage("Surname length must be between [3,10]")
                .NotEmpty().WithMessage("Surname field is empty");
            RuleFor(cmd => cmd.UserToAdd.RoleName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("RoleName field is null")
                .NotEmpty().WithMessage("RoleName field is empty");
            RuleFor(cmd => cmd.UserToAdd.Username)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Username field is null")
                .NotEmpty().WithMessage("Username field is empty");
            RuleFor(cmd => cmd.UserToAdd.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Password field is null")
                .NotEmpty().WithMessage("Password field is empty");
            RuleFor(cmd => cmd.UserToAdd.BirthDate)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("BirthDate field is null")
                .NotEmpty().WithMessage("BirthDate field is empty")
                .Matches("(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]\\d{4}").WithMessage("BirthDate string format is not valid")
                .Must(e => IsValidDateTime(e)).WithMessage("BirthDate value is not valid");
        }

        private static bool IsValidDateTime(string DateString)
        {
            var _startDate = new DateTime(1960, 1, 1);
            try {
                DateTime Date = DateTime.ParseExact(DateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                return ((DateTime.Compare(Date, _startDate) >= 0) && (DateTime.Compare(Date, DateTime.Now) < 0));
            } catch { 
                return false; 
            }
        }
    }
}
