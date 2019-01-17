using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Validators.Reminders
{
    public class ReminderValidator: AbstractValidator<Data.Reminder>
    {
        public ReminderValidator()
        {
            RuleFor(model => model.Title).NotEmpty().WithMessage("Tytuł nie może być pusty!");
            RuleFor(model => model.Email).NotEmpty().WithMessage("Email nie może być pusty!");

        }
    }
}