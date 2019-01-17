using FluentValidation;
using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reminder.Validators.Reminders
{
    public class ReminderValidator: AbstractValidator<Data.Reminder>
    {
        public ReminderValidator()
        {
            RuleFor(model => model.Title).NotEmpty().WithMessage("Tytuł nie może być pusty!");

        }
    }
}