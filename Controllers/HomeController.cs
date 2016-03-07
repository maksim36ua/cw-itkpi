﻿using System;
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
            return View(_context.Users.OrderByDescending(user => user.thisWeekHonor).ToList());
        }

        [HttpPost]
        public IActionResult Index(string value)
        {
            return RedirectToAction("Registration", "Registration");
        }

        //public IActionResult RegSuccess()
        //{
        //    return View();
        //}

        //public IActionResult RegButton()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult RegButton(string submitButton)
        //{
        //    return RedirectToAction("Registration", "Registration");
        //}

        public IActionResult Error()
        {
            return View();
        }

    }
}