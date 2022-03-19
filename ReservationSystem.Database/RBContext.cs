using ReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystem.Database
{
    public class RBContext : DbContext, IDisposable
    {
        public RBContext() : base("DefaultConnection")
        {

        }
        public DbSet<RoomSpecification> RoomSpecifications { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<Reservations> Reservations { get; set; }
    }
}
