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
    public class RoomController : Controller
    {
        // GET: Room
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
                var roomspecs = RoomSpecificationService.Instance.GetRoomSpecifications();
                return PartialView(roomspecs);
            }
        }

        [HttpPost]
        public ActionResult Create(NewRoomViewModel model)
        {
            CheckRole();
            if (role == false)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {
                var newRoom = new Room();
                newRoom.RoomName = model.RoomName;
                newRoom.RoomLocation = model.RoomLocation;
                newRoom.HaveWifi = model.HaveWifi;
                newRoom.Vacant = model.Vacant;
                newRoom.VacantBy = model.VacantBy;
                newRoom.RoomSpecification = RoomSpecificationService.Instance.GetRoomSpecification(model.RoomSpecificationID);
                RoomsService.Instance.SaveRoom(newRoom);
                return RedirectToAction("RoomsTable");
            }
        }

        public ActionResult RoomsTable(string search)
        {
            CheckRole();

            if (role == false)
            {
                return View("Login", "Login");

            }
            else
            {
                RoomSearchViewModel model = new RoomSearchViewModel();
                //if has value     //then values                    : else 1;   
                model.SearchTerm = search;
                model.Room = RoomsService.Instance.GetRooms(search);
                if (model.Room != null)
                {
                    return PartialView("RoomsTable", model);
                }
                else
                {
                    return HttpNotFound();
                }
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
                EditRoomViewModel model = new EditRoomViewModel();
                var room = RoomsService.Instance.GetRoom(ID);
                model.ID = room.ID;
                model.RoomName = room.RoomName;
                model.VacantBy = room.VacantBy;
                model.RoomLocation = room.RoomLocation;
                model.HaveWifi = room.HaveWifi;
                model.Vacant = room.Vacant;
                model.RoomSpecifications = RoomSpecificationService.Instance.GetRoomSpecifications();

                return PartialView(model);
            }

        }

        [HttpPost]
        public ActionResult Edit(EditRoomViewModel model)
        {            CheckRole();

            if (role == false)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {
                var existingProduct = RoomsService.Instance.GetRoom(model.ID);
                existingProduct.RoomName = model.RoomName;
                existingProduct.VacantBy = model.VacantBy;
                existingProduct.Vacant = model.Vacant;
                existingProduct.RoomLocation = model.RoomLocation;
                existingProduct.RoomSpecification = RoomSpecificationService.Instance.GetRoomSpecification(model.RoomSpecificationID);
                existingProduct.HaveWifi = model.HaveWifi;
                RoomsService.Instance.UpdateRoom(existingProduct);
                return RedirectToAction("RoomsTable");
            }
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            RoomsService.Instance.DeleteRoom(ID);
            return RedirectToAction("RoomsTable");
        }

    }
}