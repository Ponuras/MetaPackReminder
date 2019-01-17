using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.Reminders
{
    public class AddReminder
    {
        public void Execute(Data.Reminder model)
        {
            Data.DataContextDataContext database = new Data.DataContextDataContext();
            database.Reminders.InsertOnSubmit(model);
            database.SubmitChanges();

        }
    }
}
