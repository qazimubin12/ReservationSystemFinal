using ReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationSystem.ViewModels
{

    public class EditUserViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public List<string> AllRoles { get; set; }




    }
}