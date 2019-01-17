using FluentValidation;
using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reminder.Validators.Account
{
    public class UserRegisterValidator : AbstractValidator<RegisterModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(model => model.UserName).NotEmpty().WithMessage("Login nie może być pusty!");
            RuleFor(model => model.Password).NotEmpty().WithMessage("Hasło nie może być puste!");
            RuleFor(model => model.ConfirmPassword).NotEmpty().WithMessage("Potwierdź hasło!");

            RuleFor(model => model.ConfirmPassword).Equal(model=>model.Password).WithMessage("Hasła są różne");
        }
    }
}
