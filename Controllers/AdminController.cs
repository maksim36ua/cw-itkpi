using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw_itkpi.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;

namespace cw_itkpi.Controllers
{
    public class AdminController : Controller
    {
        private UserContext _context;

        //public AdminController(UserContext context)
        //{
        //    _context = context;
        //}

        private string password { get; set; }

        public AdminController(UserContext context)
        {
            //password = secrets.Value.password;

            var builder = new ConfigurationBuilder()
                .AddUserSecrets();
            IConfigurationRoot Configuration = builder.Build();
            password = Configuration["Data:Password"];

            _context = context;
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserInfo userObject)
        {
            if (ModelState.IsValid)
            {
                userObject.RetrieveValues();
                userObject.ClearVkLink();
                _context.Users.Update(userObject);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(userObject);
        }

        [ActionName("Delete")]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(UserInfo userInfoToDelete)
        {
            _context.Users.Remove(userInfoToDelete);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Rating()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rating(AdminClass admin)
        {
            if (admin.Password == password)
            {
                switch (admin.actionToPerform)
                {
                    case "Generate":
                        {
                            GenerateWeeklyRating();
                            break;
                        }
                    case "Update":
                        {
                            UpdateWeeklyRating();
                            break;
                        }
                    case "Delete":
                        {
                            DeleteWeeklyRating();
                            break;
                        }
                    default:
                        return RedirectToAction("Index", "Home");
                }

                return RedirectToAction("Index", "Home");
            }
            else
                return View();
        }

        public void UpdateWeeklyRating()
        {
            _context.Users.ToList()
                .ForEach(user =>
                {
                    user.RetrieveValues();
                    user.thisWeekHonor = user.honor - user.lastWeekHonor;
                    _context.Entry(user).State = Microsoft.Data.Entity.EntityState.Modified;
                });
            _context.SaveChanges();
        }

        public void GenerateWeeklyRating()
        {
            _context.Users.ToList()
                .ForEach(user =>
                {
                    user.RetrieveValues();
                    user.pointsHistory += " " + user.honor;
                    user.lastWeekHonor = user.honor;
                    _context.Entry(user).State = Microsoft.Data.Entity.EntityState.Modified;
                });
            _context.SaveChanges();

            UpdateWeeklyRating();
        }        

        public void DeleteWeeklyRating()
        {
            _context.Users.ToList()
                .ForEach(user =>
                {
                    user.RetrieveValues();

                    if (user.pointsHistory.Contains(" "))
                    {
                        var lastIndex = user.pointsHistory.LastIndexOf(' ');
                        user.pointsHistory = user.pointsHistory.Substring(0, lastIndex);

                        var pointsArray = user.pointsHistory.Split(' ');  // Take last week honor for the basis
                        user.lastWeekHonor = int.Parse(pointsArray.Last());
                    }
                    else
                    {
                        user.lastWeekHonor = 0;
                    }

                    _context.Entry(user).State = Microsoft.Data.Entity.EntityState.Modified;
                });
            _context.SaveChanges();

            UpdateWeeklyRating();
        }
    }
}
