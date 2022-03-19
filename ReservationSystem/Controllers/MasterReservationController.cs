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
    public class MasterReservationController : Controller
    {
        // GET: MasterReservation
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
        public ActionResult Create(int ID)
        {
            EditReservationViewModel model = new EditReservationViewModel();
            model.Room = RoomsService.Instance.GetRoomWithSpecs(ID);
            if (model.Room.HaveWifi == false)
            {
                ViewBag.havewifi = "No";
            }
            else
            {
                ViewBag.havewifi = "Yes";
            }
            return PartialView(model.Room);

        }




        [HttpPost]
        public ActionResult Create(NewReservationViewModel model)
        {
            var newReservation = new Reservations();
            newReservation.ReservationTime = model.ReservationTime;
            newReservation.ReservationAgenda = model.ReservationAgenda;
            newReservation.BookedBy = model.BookedBy;
            newReservation.VacantBy = model.VacantBy;
            newReservation.RoomSpecification = RoomSpecificationService.Instance.GetRoomSpecification(model.RoomSpecificationID);
            newReservation.Room = RoomsService.Instance.GetRoom(model.RoomID);
            ReservationService.Instance.SaveReservation(newReservation);
            RoomsService.Instance.UpdateVacantByandVacany(model.RoomID, newReservation.VacantBy);
            return RedirectToAction("MasterReservationTable");
        }




        public ActionResult MasterReservationTable(ReservationSearchViewModel model)
        {
            
            model.Reservations = MasterReservationsService.Instance.GetMasterReservations();
            model.Rooms = RoomsService.Instance.GetNonVacantRooms();
            if (model != null)
            {
                return PartialView("MasterReservationTable", model);
            }
            else
            {
                return HttpNotFound();
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

                EditReservationViewModel model = new EditReservationViewModel();
                var masterreservation = MasterReservationsService.Instance.GetMasterReservation(ID);
                model.ID = masterreservation.ID;
                model.Rooms = RoomsService.Instance.GetAvailableRooms();
                model.RoomSpecification = masterreservation.RoomSpecification;
                model.ReservationAgenda = masterreservation.ReservationAgenda;
                model.ReservationTime = masterreservation.ReservationTime;
                model.VacantBy = masterreservation.VacantBy;
                model.BookedBy = masterreservation.BookedBy;
                if (masterreservation.Room.HaveWifi == false)
                {
                    ViewBag.havewifi = "No";
                }
                else
                {
                    ViewBag.havewifi = "Yes";
                }
                return PartialView(model);
            }
        }




        [HttpPost]
        public ActionResult Edit(EditReservationViewModel model)
        {
            CheckRole();
            if (role == false)
            {
                return RedirectToAction("Login", "Login");

            }
            else
            {
                var existingReservation = MasterReservationsService.Instance.GetMasterReservation(model.ID);

                RoomsService.Instance.RelaseUpdateVacantByandVacany(existingReservation.Room.ID); // Room Vacancy is setting to True (Previous Room)
                RoomsService.Instance.UpdateVacantByandVacany(model.RoomID, model.VacantBy); //Room Vacancy is setting to False (new Room)

               // var newroom = RoomsService.Instance.GetRoomWithSpecs(model.RoomID);
                MasterReservationsService.Instance.UpdateRoomInReservation(model.ID,model.RoomID); //Changing Room in Reservation

                existingReservation.ReservationAgenda = model.ReservationAgenda;
                existingReservation.ReservationTime = model.ReservationTime;
                int specsId = RoomsService.Instance.GetRoomSpecs(model.RoomID);
                existingReservation.RoomSpecification = RoomSpecificationService.Instance.GetRoomSpecification(specsId);
                existingReservation.VacantBy = model.VacantBy;
                existingReservation.BookedBy = model.BookedBy;
                MasterReservationsService.Instance.UpdateMasterReservation(existingReservation);
                return RedirectToAction("MasterReservationTable");

            }

        }




        [HttpPost]
        public ActionResult Delete(ReservationSearchViewModel model)
        {
            var existingReservation = ReservationService.Instance.GetReservationsWithRoom(model.ID);
            RoomsService.Instance.RelaseUpdateVacantByandVacany(existingReservation.Room.ID);

            MasterReservationsService.Instance.DeleteMasterReservation(existingReservation.ID);
            return RedirectToAction("MasterReservationTable");
        }



    }
}