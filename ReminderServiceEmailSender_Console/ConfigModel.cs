using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ReminderServiceEmailSender_Console
{
    public class ConfigModel
    {
        public int Port = 587;
        public string Host = "smtp.gmail.com";
        public bool EnableSsl = true;
        public int Timeout = 10000;
        public int DeliveryMethod = (int)SmtpDeliveryMethod.Network;
        public bool UseDefaultCredentials = false;
        public string login = "user@gmail.com";
        public string password = "password";
    }
}
