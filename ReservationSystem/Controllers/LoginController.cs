using ReservationSystem.Database;
using ReservationSystem.Entities;
using ReservationSystem.Services;
using ReservationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            UsersService.Instance.SaveUser(user);
            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            using(var context = new RBContext())
            {
                var user = context.Users.Single(u => u.Email == model.Email && u.Password == model.Password);
                if(user != null)
                {
                    Session["ID"] = user.ID.ToString();
                    Session["UserName"] = user.UserName.ToString();
                    Session["FirstName"] = user.FirstName.ToString();
                    Session["Email"] = user.Email.ToString();
                    Session["Role"] = user.Role.ToString();
                    if(user.Role.ToString() == "Admin")
                    {
                        return RedirectToAction("AdminDashboard");
                    }
                    else
                    {
                        return RedirectToAction("UserDashboard");

                    }
                }
                else
                {

                    Session["ID"] = null;
                    Session["UserName"] = null;
                    Session["Role"] = null;
                    return RedirectToAction("Login");

                }

            }
            return View();
        }

       


        public ActionResult AdminDashboard()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login");
            }
            string[] roles = System.Web.Security.Roles.GetRolesForUser(Convert.ToString(Session["UserName"]));
            if (roles[0] == "Admin")
            {
               return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult UserDashboard()
        {
            string[] roles = System.Web.Security.Roles.GetRolesForUser(Convert.ToString(Session["UserName"]));
            if (roles[0] == "User")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}