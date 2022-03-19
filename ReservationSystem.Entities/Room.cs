using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Entities
{
    public class Room : BaseEntity
    {
        public virtual RoomSpecification RoomSpecification { get; set; }

        [Required]
        public string RoomName { get; set; }

        public string RoomLocation { get; set; }
        public Boolean HaveWifi { get; set; }
        public Boolean Vacant { get; set; }
        public string VacantBy { get; set; }

    }


   
    
}
