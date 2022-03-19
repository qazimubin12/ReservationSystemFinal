using ReservationSystem.Entities;
using ReservationSystem.Services;
using ReservationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ReservationSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        bool role = false;
        public void CheckRole()
        {
            role = false;
            if (Session["UserName"] == null)
            {
                role = false;
            }
            string[] roles = System.Web.Security.Roles.GetRolesForUser(Convert.ToString(Session["UserName"]));
            if (roles[0] == "Admin")
            {
                role = true;

            }
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult UsersTable()
        {
            CheckRole();

            if (role == false)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {

                var users = UsersService.Instance.GetUsers();
                return PartialView(users);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            CheckRole();

            if (role == false)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {
                return PartialView();
            }
        }

        [HttpPost]
        public ActionResult Create(Users user)
        {
            CheckRole();

            if (role == false)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {
                UsersService.Instance.SaveUser(user);
                return RedirectToAction("UsersTable");
            }
            
        }


        [HttpGet]
        public ActionResult Edit(int ID)
        {

            EditUserViewModel model = new EditUserViewModel();
            var user = UsersService.Instance.GetUser(ID);
            model.ID = user.ID;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.UserName = user.UserName;
            model.Password = user.Password;
            model.PhoneNumber = user.PhoneNumber;
            model.Department = user.Department;
            model.Roles = user.Role;
            ViewBag.User_Role = model.Roles;
            List<string> allroles = new List<string>();
            allroles.Add("Admin");
            allroles.Add("User");
            model.AllRoles = allroles;
            model.Email = user.Email;

            return PartialView(model);

        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {

            var existingUser = UsersService.Instance.GetUser(model.ID);
            existingUser.FirstName = model.FirstName;
            existingUser.LastName = model.LastName;
            existingUser.UserName = model.UserName;
            existingUser.Password = model.Password;
            existingUser.PhoneNumber = model.PhoneNumber;
            existingUser.Department = model.Department;
            existingUser.Email = model.Email;
            existingUser.Role = model.Roles;
            UsersService.Instance.UpdateUser(existingUser);

            return RedirectToAction("UsersTable");


        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            CheckRole();

            if (role == false)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {
                UsersService.Instance.DeleteUser(ID);
                return RedirectToAction("UsersTable");
            }
        }




      

        [HttpPost]
        public ActionResult EditUser(EditUserViewModel model)
        {

            int id = int.Parse(Session["ID"].ToString());
            var existingUser = UsersService.Instance.GetUser(id);

            existingUser.FirstName = model.FirstName;
            existingUser.LastName = model.LastName;
            existingUser.UserName = model.UserName;
            existingUser.Password = model.Password;
            existingUser.PhoneNumber = model.PhoneNumber;
            existingUser.Department = model.Department;
            existingUser.Email = model.Email;
            UsersService.Instance.UpdateUser(existingUser);
            return RedirectToAction("UsersEditTable");

        }


        public ActionResult UsersEditTable()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {
                int id = int.Parse(Session["ID"].ToString());
                EditUserViewModel model = new EditUserViewModel();
                var user = UsersService.Instance.GetUser(id);
                model.ID = user.ID;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.UserName = user.UserName;
                model.Password = user.Password;
                model.PhoneNumber = user.PhoneNumber;
                model.Department = user.Department;
                model.Email = user.Email;

                return PartialView(model);
            }
        }
        
    }
}