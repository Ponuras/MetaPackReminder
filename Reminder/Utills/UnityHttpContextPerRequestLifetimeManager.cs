using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reminder.Utills
{
    public class UnityHttpContextPerRequestLifetimeManager : UnityPerRequestLifetimeManager
    {
        public UnityHttpContextPerRequestLifetimeManager()
            : base(new HttpContextPerRequestStore())
        {
        }
    }
}