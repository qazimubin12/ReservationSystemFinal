using ReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationSystem.ViewModels
{
    public class ReservationSearchViewModel
    {
        
        public int ID { get; set; }
        public List<Reservations> Reservations { get; set; }
        public List<Room> Rooms { get; set; }
        public string SearchTerm { get; set; }
        public int RoomSpecificationID { get; set; }
        public Reservations Reservation { get; set; }


        public Room Room { get; set; }

        public int RoomID { get; set; }
    }






    public class EditReservationViewModel
    {
        public int ID { get; set; }

        public virtual Room Room { get; set; }
        public virtual RoomSpecification RoomSpecification { get; set; }

        public DateTime ReservationTime { get; set; }
        public string MeetingStatus { get; set; }
        public string ReservationAgenda { get; set; }
        public string VacantBy { get; set; }
        public List<Reservations> Reservations { get; set; }

        public string BookedBy { get; set; }

        public int RoomID { get; set; }
        public int RoomSpecificationID { get; set; }
        public List<Room> Rooms { get; set; }




    }
    public class NewReservationViewModel
    {
        public int RoomID { get; set; }
        public int RoomSpecificationID { get; set; }
        public string VacantBy { get; set; }
        public string BookedBy { get; set; }
        public string MeetingStatus { get; set; }

        public DateTime ReservationTime { get; set; }

        public string ReservationAgenda { get; set; }

        public List<Room> Rooms { get; set; }
    }


    public class MasterReservationViewModel
    {
        public int ID { get; set; }

        public virtual Room Room { get; set; }
        public virtual RoomSpecification RoomSpecification { get; set; }

        public DateTime ReservationTime { get; set; }
        public string ReservationAgenda { get; set; }
        public string MeetingStatus { get; set; }
        public string VacantBy { get; set; }
        public string BookedBy { get; set; }

        public List<Reservations> Reservations { get; set; }
        public List<Room> Rooms { get; set; }
    }


}