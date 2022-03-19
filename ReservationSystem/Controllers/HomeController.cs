using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationSystem.Database;
using ReservationSystem.Services;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();

        }

        [Authorize]
        public ActionResult UserDashboard()
        {
            return View();

        }

        [Authorize]

        public ActionResult AdminDashboard()
        {
  
            return View();

        }


        public ActionResult LogOut()
        {
            Session["ID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["Email"] = string.Empty;
            Session["PhoneNumber"] = string.Empty;
            Session["Roles"] = string.Empty;
            Session["FirstName"] = string.Empty;
            Session["LastName"] = string.Empty;
            Session["Department"] = string.Empty;
            return RedirectToAction("Login","Login");
        }

    }
}