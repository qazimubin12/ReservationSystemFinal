using ReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationSystem.ViewModels
{


    public class RoomSearchViewModel
    {
        public List<Room> Room { get; set; }
        public string SearchTerm { get; set; }
    }


    public class EditRoomViewModel
    {
        public int ID { get; set; }

        public int RoomSpecificationID { get; set; }
        public string RoomName { get; set; }

        public string RoomLocation { get; set; }
        public Boolean HaveWifi { get; set; }
        public Boolean Vacant { get; set; }
        public string VacantBy { get; set; }

        public List<RoomSpecification> RoomSpecifications { get; set; }
    }
    public class NewRoomViewModel
    {
        public int RoomSpecificationID { get; set; }
        public string RoomName { get; set; }
        public string RoomLocation { get; set; }

        public Boolean HaveWifi { get; set; }
        public Boolean Vacant { get; set; }
        public string VacantBy { get; set; }
    }
}