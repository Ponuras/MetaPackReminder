using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ReminderModel
    {
        public ReminderModel() { }
        public ReminderModel(Data.Reminder r)
        {
            this.Title = r.Title;
            this.Description = r.Description;
            this.Users = r.ReminderUsers.Select(u => new User(u.UserName, u.Email)).ToList();
            this.Owner = r.Owner;
            this.Email = r.Email;
            this.ID = r.Id;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<User> Users { get; set; }

        public string Owner { get; set; }

        public string Email { get; set; }

        public Guid ID { get; set; }
    }

    public class User
    {
        public User() { }

        public User(string UserName, string Email)
        {
            // TODO: Complete member initialization
            this.UserName = UserName;
            this.Email = Email;
        }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
