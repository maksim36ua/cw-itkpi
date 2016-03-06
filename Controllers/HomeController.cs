using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using cw_itkpi.Models;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace itkpi_cw.Controllers
{
    public class HomeController : Controller
    {
        private UserContext _context;

        public HomeController(UserContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            _context.Users.OrderByDescending(user => user.thisWeekHonor);
            _context.SaveChanges();

            return View(_context.Users.ToList());
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(UserInfo user)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(user.RetrieveValues()))
            {
                user.ClearVkLink();
                user.thisWeekHonor = user.honor - user.lastWeekHonor;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}