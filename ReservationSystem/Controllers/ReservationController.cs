using ReservationSystem.Database;
using ReservationSystem.Entities;
using ReservationSystem.Services;
using ReservationSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ReservationSystem.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        public ActionResult Index()
        {
            
            return View();
        }


        [HttpGet]
        public ActionResult Create(int ID)
        {
            NewReservationViewModel model = new NewReservationViewModel();
            model.Room = RoomsService.Instance.GetRoomWithSpecs(ID);
            if(model.Room.HaveWifi == false)
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
            newReservation.MeetingStatus = "In Process";
            newReservation.RoomSpecification = RoomSpecificationService.Instance.GetRoomSpecification(model.RoomSpecificationID);
            newReservation.Room = RoomsService.Instance.GetRoom(model.RoomID);
            ReservationService.Instance.SaveReservation(newReservation);
            RoomsService.Instance.UpdateVacantByandVacany(model.RoomID, newReservation.VacantBy);
            return RedirectToAction("ReservationTable");
        }



        public JsonResult SendMailToUser(NewReservationViewModel r)
        {
            bool result = false;
            var roomname = RoomsService.Instance.GetRoomName(r.RoomID);
            var vacantby = r.VacantBy;
            var bookedby = r.BookedBy;
            var agenda = r.ReservationAgenda;
            var time = r.ReservationTime;
            result = SendEmail(Convert.ToString(Session["Email"]), "Reservation Booking", "<p> Hi,"+Session["FirstName"]+" Your Room Reservations is confirmed. In case of any assistance contact IT. <br /> <b>Room Name:</b>" + roomname + "<br /> <b>Vacant By:</b>" + vacantby + "<br /> <b>Booking By:</b>" + bookedby + "<br /><b>Agenda:</b>" + agenda + "<br /> <b>Booking Time:</b>" + time + "</p>");
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["smtpUserEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["smtpPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }


        public ActionResult ReservationTable(ReservationSearchViewModel model)
        {
            model.Rooms = RoomsService.Instance.GetAvailableRooms();
            if (model.Rooms != null)
            {
                return PartialView("ReservationTable", model);
            }
            else
            {
                return HttpNotFound();
            }

        }


        [HttpGet]
        public ActionResult Edit(int ID)
        {

            EditReservationViewModel model = new EditReservationViewModel();
            var reservation = ReservationService.Instance.GetReservation(ID);
            model.ID = reservation.ID;
            model.ReservationAgenda = reservation.ReservationAgenda;
            model.ReservationTime = reservation.ReservationTime;
            model.MeetingStatus = reservation.MeetingStatus;
            model.RoomSpecification = RoomSpecificationService.Instance.GetRoomSpecification(model.RoomSpecification.ID);
            model.Room = RoomsService.Instance.GetRoom(model.Room.ID);
            return PartialView(model);


        }

        [HttpPost]
        public ActionResult Edit(EditReservationViewModel model)
        {
            var existignReservation = ReservationService.Instance.GetReservation(model.ID);
            existignReservation.Room =  RoomsService.Instance.GetRoom(model.Room.ID);
            existignReservation.ReservationAgenda = model.ReservationAgenda;
            existignReservation.ReservationTime = model.ReservationTime;
            existignReservation.MeetingStatus = model.MeetingStatus;
            existignReservation.RoomSpecification = RoomSpecificationService.Instance.GetRoomSpecification(model.RoomSpecification.ID);
            ReservationService.Instance.UpdateReservation(existignReservation);
            return RedirectToAction("ReservationTable");

        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ReservationService.Instance.DeleteReservation(ID);
            return RedirectToAction("ReservationTable");
        }


        public List<Reservations> GetMyReservations()
        {
            var bookedby = Convert.ToString(Session["UserName"]);
            using (var context = new RBContext())
            {

                return context.Reservations.Include(x=>x.Room).Include(x=>x.RoomSpecification)
                    .Where(x => x.BookedBy == bookedby)
                    .ToList();

            }
        }


        public ActionResult ViewMyReservations(ReservationSearchViewModel model)
        {

            model.Reservations = GetMyReservations();
            if (model != null)
            {
                return PartialView("ViewMyReservations", model);
            }
            else
            {
                return HttpNotFound();
            }

        }


        [HttpPost]
        public ActionResult EndMeeting(int ID)
        {

            int roomid = ReservationService.Instance.GetRoomID(ID);
            RoomsService.Instance.RelaseUpdateVacantByandVacany(roomid);
            ReservationService.Instance.EndMeeting(ID);
            return RedirectToAction("ViewMyReservations");
        }


        public ActionResult MyReseservationIndex()
        {

            return View();
        }





    }
}