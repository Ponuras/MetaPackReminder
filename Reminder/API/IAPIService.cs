using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Reminder.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAPIService" in both code and config file together.
    [ServiceContract]
    public interface IAPIService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        string GetReminders();


        [OperationContract]
        void DeleteReminder(Guid ID);
    }
}
