using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Entities
{
    public class Reservations : BaseEntity
    {
        public  RoomSpecification RoomSpecification { get; set; }
        public  Room Room { get; set; }

        public DateTime ReservationTime { get; set; }

        public string ReservationAgenda { get; set; }
        public string VacantBy { get; set; }
        public string BookedBy { get; set; }
        public string MeetingStatus { get; set; }
    }
}
