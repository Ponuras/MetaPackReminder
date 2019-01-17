using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain.Handlers.Reminders
{
    public class GetRemindersList
    {
        public List<Reminder> Execute(Expression<Func<Reminder, bool>> predicate)
        {
            Data.DataContextDataContext database = new Data.DataContextDataContext();
            return database.Reminders.Where(predicate).ToList();

        }
       
    }
}
