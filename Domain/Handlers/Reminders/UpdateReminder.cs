using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.Reminders
{
    public class UpdateReminder
    {
        public void Execute(Reminder model)
        {
            Data.DataContextDataContext database = new Data.DataContextDataContext();
            var old = database.Reminders.Where(r => r.Id == model.Id).SingleOrDefault();
            old.Group = model.Group;
            old.Owner = model.Owner;
            old.RemindDate = model.RemindDate;
            old.Title = model.Title;
            old.Description = model.Description;


            foreach (var item in model.Tags.Where(t => !old.Tags.Contains(t)))
            {
                old.Tags.Remove(item);
            }
            foreach (var item in model.ReminderUsers.Where(t => !old.ReminderUsers.Contains(t)))
            {
                old.ReminderUsers.Remove(item);
            }
            old.ReminderUsers = model.ReminderUsers;
            old.Tags = model.Tags;
            old.Email = model.Email;

            database.SubmitChanges();

        }
    }
}
