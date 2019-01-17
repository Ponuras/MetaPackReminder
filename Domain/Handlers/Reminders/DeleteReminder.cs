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
    public class DeleteReminder
    {
        public void Execute(Guid ID)
        {
            Data.DataContextDataContext database = new Data.DataContextDataContext();


            database.Reminders.DeleteOnSubmit(database.Reminders.Where(r => r.Id == ID).SingleOrDefault());


            database.SubmitChanges();
        }
       
    }
}
