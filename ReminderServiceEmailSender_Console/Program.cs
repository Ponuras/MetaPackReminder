using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ReminderServiceEmailSender_Console
{
    class Program
    {
        static Task tasker = new Task(new Action(() => { EmailSender(); }));
        static bool taskBreaker = false;

        static WebAPIService.APIServiceClient API = new WebAPIService.APIServiceClient();

        static void Main(string[] args)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//config.xml"))
            {
                Console.WriteLine("Hola, hola! Moj program o configa wola!");
                Console.WriteLine("Tutaj wstawiam domyślnego xml'a do uzupełnienia: " + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//config.xml");
                SetDefaultConfig();
                Console.WriteLine("Wciśnij DOWOLNY klawisz aby kontynuować");
                Console.ReadKey();
            }
            else
            {
                tasker.Start();
                Console.ReadKey();
            }

            while (tasker.Status == TaskStatus.Running)
            {

                Console.Clear();
                Console.WriteLine("Wciśnij ESC klawisz aby zakończyć.");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    taskBreaker = true;
                }

            }
        }


        private static void EmailSender()
        {
            SmtpClient client = new SmtpClient();
            client = GetConfig();

            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(ConfigModel));
            System.IO.StreamReader file = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//config.xml");
            var config = (ConfigModel)reader.Deserialize(file);


            while (taskBreaker == false)
            {
                foreach (var item in GetReminders(API.GetReminders()))
                {
                    foreach (var user in item.Users.Where(u => !String.IsNullOrEmpty(u.Email)))
                    {
                        using (MailMessage mm = new MailMessage(config.login, user.Email, item.Title, item.Description))
                        {
                            mm.BodyEncoding = UTF8Encoding.UTF8;
                            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                            client.Send(mm);
                            Console.WriteLine("Wysłano do " + user.Email);
                        }
                    }

                    using (MailMessage mm = new MailMessage(config.login, item.Email, item.Title, item.Description))
                    {
                        mm.BodyEncoding = UTF8Encoding.UTF8;
                        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                        client.Send(mm);
                        Console.WriteLine("Wysłano do " + item.Email);
                    }
                    API.DeleteReminder(item.ID);
                }
            }
            Console.WriteLine("Koniec!");
        }

        private static List<Domain.Models.ReminderModel> GetReminders(string RemindersXML)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Domain.Models.ReminderModel>));
            IEnumerable<Domain.Models.ReminderModel> result;

            using (TextReader reader = new StringReader(RemindersXML))
            {
                result = (IEnumerable<Domain.Models.ReminderModel>)serializer.Deserialize(reader);
            }

            return result.ToList();
        }


        private static SmtpClient GetConfig()
        {
            SmtpClient client = new SmtpClient();

            // Now we can read the serialized book ...  
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(ConfigModel));
            System.IO.StreamReader file = new System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//config.xml");
            var config = (ConfigModel)reader.Deserialize(file);

            client.UseDefaultCredentials = config.UseDefaultCredentials;
            client.Credentials = new System.Net.NetworkCredential(config.login, config.password);
            client.Port = config.Port;
            client.Host = config.Host;
            client.EnableSsl = config.EnableSsl;
            client.Timeout = config.Timeout;
            client.DeliveryMethod = (SmtpDeliveryMethod)config.DeliveryMethod;


            file.Close();


            return client;
        }


        private static void SetDefaultConfig()
        {

            ConfigModel client = new ConfigModel();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = (int)SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.login = "user@gmail.com";
            client.password = "password";

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(ConfigModel));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//config.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, client);
            file.Close();

        }


    }
}
