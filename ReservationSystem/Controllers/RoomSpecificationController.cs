using ReservationSystem.Entities;
using ReservationSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationSystem.Controllers
{
    public class RoomSpecificationController : Controller
    {
        // GET: RoomSpecification
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

        public ActionResult RoomSpecificationTable()
        {
            CheckRole();
            if (role == false)
            {
                return RedirectToAction("Login", "Login");
            }

            else
            {
                var roomspecs = RoomSpecificationService.Instance.GetRoomSpecifications();
                return PartialView(roomspecs);
            }
        }


        [HttpGet]
        public ActionResult Index()
        {
            CheckRole();
            if(role == false)
            { 
                return RedirectToAction("Login", "Login");

            }
            else
            {
                return View();

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
        public ActionResult Create(RoomSpecification roomSpecification)
        {
            CheckRole();
            if (role == false)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {
                RoomSpecificationService.Instance.SaveRoomSpecification(roomSpecification);
                return RedirectToAction("RoomSpecificationTable");
            }
        }


        [HttpGet]
        public ActionResult Edit(int ID)
        {
            CheckRole();
            if (role == false)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                var RoomSpecification = RoomSpecificationService.Instance.GetRoomSpecification(ID);
                return PartialView(RoomSpecification);
            }
        }

        [HttpPost]
        public ActionResult Edit(RoomSpecification roomSpecification)
        {   CheckRole();
            if (role == false)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {
                RoomSpecificationService.Instance.UpdateRoomSpecification(roomSpecification);
                return RedirectToAction("RoomSpecificationTable");
            }
      
        }



        [HttpPost]
        public ActionResult Delete(int ID)
        {
            RoomSpecificationService.Instance.DeleteRoomSpecification(ID);
            return RedirectToAction("RoomSpecificationTable");
        }


    }
}