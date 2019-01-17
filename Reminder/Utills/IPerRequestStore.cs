using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Utills
{
    public interface IPerRequestStore
    {
        object GetValue(object key);
        void SetValue(object key, object value);
        void RemoveValue(object key);

        event EventHandler EndRequest;
    }
}
