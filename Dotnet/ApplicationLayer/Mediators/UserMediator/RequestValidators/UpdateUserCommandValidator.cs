using DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Commands;
using FluentValidation;
using System.Globalization;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.RequestValidators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly DateTime _startDate = new DateTime(1960, 1, 1);
        public UpdateUserCommandValidator() 
        {
            RuleFor(cmd => cmd.UserToUpdate.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User.Name field is null")
                .Length(3, 10).WithMessage("User.Name length must be between [3,10]")
                .NotEmpty().WithMessage("User.Name field is empty");
            RuleFor(cmd => cmd.UserToUpdate.Surname)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User.Surname field is null")
                .Length(3, 10).WithMessage("User.Surname length must be between [3,10]")
                .NotEmpty().WithMessage("User.Surname field is empty");
            RuleFor(cmd => cmd.UserToUpdate.RoleName)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User.RoleName field is null")
                .NotEmpty().WithMessage("User.RoleName field is empty");
            RuleFor(cmd => cmd.UserToUpdate.Username)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User.Username field is null")
                .NotEmpty().WithMessage("User.Username field is empty");
            RuleFor(cmd => cmd.UserToUpdate.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User.Password field is null")
                .NotEmpty().WithMessage("User.Password field is empty");
            RuleFor(cmd => cmd.UserToUpdate.BirthDate)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User.BirthDate field is null")
                .NotEmpty().WithMessage("User.BirthDate field is empty")
                .Must(e => IsValidDateTime(e)).WithMessage("User.BirthDate is not valid");
        }

        private bool IsValidDateTime(string DateString)
        {
            DateTime Date = DateTime.ParseExact(DateString, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return ((DateTime.Compare(Date, _startDate) >= 0) && (DateTime.Compare(Date, DateTime.Now) < 0));
        }
    }
}
