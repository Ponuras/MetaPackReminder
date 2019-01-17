using FluentValidation;
using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reminder.Validators.Account
{
    public class UserLoginValidator: AbstractValidator<LoginModel>
    {
        public UserLoginValidator()
        {
            RuleFor(model => model.UserName).NotEmpty().WithMessage("Login nie może być pusty!");
            RuleFor(model => model.Password).NotEmpty().WithMessage("Hasło nie może być puste!");

        }
    }
}