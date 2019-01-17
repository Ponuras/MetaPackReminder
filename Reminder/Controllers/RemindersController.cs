
using Data.Validators.Reminders;
using Microsoft.Practices.ServiceLocation;
using Reminder.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Reminder.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class RemindersController : Controller
    {
        //
        // GET: /Reminders/

        public ActionResult Index()
        {
            var model = new Domain.Handlers.Reminders.GetRemindersList().Execute(r => r.Owner == User.Identity.Name || r.ReminderUsers.Any(u=>u.UserName==User.Identity.Name)).OrderByDescending(r=>r.RemindDate).ToList();
            ViewBag.Groups = new Domain.Handlers.Groups.GetGroupsList().Execute(r => true);


            return View(model);
        }


        [HttpGet]
        public ActionResult Delete(Guid ID)
        {
            new Domain.Handlers.Reminders.DeleteReminder().Execute(ID);
            ViewBag.Groups = new Domain.Handlers.Groups.GetGroupsList().Execute(r => true);

            var model = new Domain.Handlers.Reminders.GetRemindersList().Execute(r => r.Owner == User.Identity.Name || r.ReminderUsers.Any(u => u.UserName == User.Identity.Name)).OrderByDescending(r=>r.RemindDate).ToList();



            return PartialView("_Index",model);
        }


        [HttpGet]
        public ActionResult Edit(Guid ID)
        {
            Data.Reminder model = new Data.Reminder();
            model = new Domain.Handlers.Reminders.GetRemindersList().Execute(r => r.Id == ID).SingleOrDefault();

            ViewBag.Groups = new SelectList(new Domain.Handlers.Groups.GetGroupsList().Execute(r => true), "Id", "GroupName");
            ViewBag.Users = new SelectList(new Domain.Handlers.Users.GetUsers().Execute(u => u.UserName != User.Identity.Name), "UserName", "UserName");

            Session["Tags"] = model.Tags.Select(t => t.Tag1).ToList();
            Session["Users"] = model.ReminderUsers.Select(b => new Tuple<string,string>(b.UserName, b.Email)).ToList();

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(Data.Reminder model)
        {
            model.Owner = User.Identity.Name;


            ViewBag.Groups = new SelectList(new Domain.Handlers.Groups.GetGroupsList().Execute(r => true), "Id", "GroupName");
            ViewBag.Users = new SelectList(new Domain.Handlers.Users.GetUsers().Execute(u => u.UserName != User.Identity.Name), "UserName", "UserName");

            if (((List<string>)Session["Tags"]) != null)
            {

                model.Tags.AddRange(((List<string>)Session["Tags"]).Select(t => new Data.Tag() { Tag1 = t, Id = Guid.NewGuid() }).ToList());
            }

            if (((List<Tuple<string, string>>)Session["Users"]) != null)
            {
                model.ReminderUsers.AddRange(((List<Tuple<string, string>>)Session["Users"]).Select(u => new Data.ReminderUser() { Email = u.Item2, UserName = u.Item1, Id = Guid.NewGuid() }).ToList());

            }
            model.Owner = User.Identity.Name;

            ReminderValidator validator = new ReminderValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                return View(model);
            }
            if (String.IsNullOrEmpty(model.Description))
            {
                model.Description = "";
            }
            new Domain.Handlers.Reminders.UpdateReminder().Execute(model);

            return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Groups = new SelectList(new Domain.Handlers.Groups.GetGroupsList().Execute(r => true), "Id", "GroupName");
            ViewBag.Users = new SelectList(new Domain.Handlers.Users.GetUsers().Execute(u => u.UserName != User.Identity.Name), "UserName", "UserName");

            return View(new Data.Reminder() { RemindDate = DateTime.Today, Description = "" });
        }

        [HttpPost]
        public ActionResult Add(Data.Reminder model)
        {
            model.Owner = User.Identity.Name;


            ViewBag.Groups = new SelectList(new Domain.Handlers.Groups.GetGroupsList().Execute(r => true), "Id", "GroupName");
            ViewBag.Users = new SelectList(new Domain.Handlers.Users.GetUsers().Execute(u => u.UserName != User.Identity.Name), "UserName", "UserName");

            if (((List<string>)Session["Tags"]) != null)
                model.Tags.AddRange(((List<string>)Session["Tags"]).Select(t => new Data.Tag() { Tag1 = t, Id = Guid.NewGuid() }).ToList());

            if (((List<Tuple<string, string>>)Session["Users"]) != null)
                model.ReminderUsers.AddRange(((List<Tuple<string, string>>)Session["Users"]).Select(u => new Data.ReminderUser() { Email = u.Item2, UserName = u.Item1, Id = Guid.NewGuid() }).ToList());
            model.Owner = User.Identity.Name;

            ReminderValidator validator = new ReminderValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                return View(model);
            }
            if (String.IsNullOrEmpty(model.Description))
            {
                model.Description = "";
            }
            model.Id = Guid.NewGuid();
            new Domain.Handlers.Reminders.AddReminder().Execute(model);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult AddUser(string User, string Email)
        {
            if (((List<Tuple<string, string>>)Session["Users"]) == null)
            {
                Session["Users"] = new List<Tuple<string, string>>();
            }
            ((List<Tuple<string, string>>)Session["Users"]).Add(new Tuple<string, string>(User, Email));

            return PartialView("_Users", ((List<Tuple<string, string>>)Session["Users"]));
        }

        [HttpGet]
        public ActionResult RemoveUser(int ID)
        {
            if (((List<Tuple<string, string>>)Session["Users"]) == null)
            {
                Session["Users"] = new List<Tuple<string, string>>();
            }
            ((List<Tuple<string, string>>)Session["Users"]).RemoveAt(ID);

            return PartialView("_Users", ((List<Tuple<string, string>>)Session["Users"]));
        }


        [HttpPost]
        public ActionResult AddTag(string Tag)
        {
            if (((List<string>)Session["Tags"]) == null)
            {
                Session["Tags"] = new List<string>();
            }
            ((List<string>)Session["Tags"]).Add(Tag);

            return PartialView("_Tags", ((List<string>)Session["Tags"]));
        }

        [HttpGet]
        public ActionResult RemoveTag(int ID)
        {
            if (((List<string>)Session["Tags"]) == null)
            {
                Session["Tags"] = new List<string>();
            }
            ((List<string>)Session["Tags"]).RemoveAt(ID);

            return PartialView("_Tags", ((List<string>)Session["Tags"]));
        }



    }
}
