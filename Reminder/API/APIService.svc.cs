using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Reminder.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "APIService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select APIService.svc or APIService.svc.cs at the Solution Explorer and start debugging.
    public class APIService : IAPIService
    {
        public void DoWork()
        {
        }


        public string GetReminders()
        {
            try
            {
                MemoryStream mr = new MemoryStream();
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Domain.Models.ReminderModel>));
                reader.Serialize(mr,new Domain.Handlers.Reminders.GetRemindersList().Execute(r => r.RemindDate <= DateTime.Now).Select(r=>new Domain.Models.ReminderModel(r)).ToList());
                mr.Position = 0;
                
                return  new StreamReader(mr).ReadToEnd();
            }
            catch (Exception ex)
            {
return "";
            } 
        }


        public void DeleteReminder(Guid ID)
        {
            new Domain.Handlers.Reminders.DeleteReminder().Execute(ID);
        }
    }
}
