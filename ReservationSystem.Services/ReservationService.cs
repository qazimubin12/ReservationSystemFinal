using ReservationSystem.Database;
using ReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ReservationSystem.Services
{
    public class ReservationService
    {
        #region Singleton
        public static ReservationService Instance
        {
            get
            {
                if (instance == null) instance = new ReservationService();
                return instance;
            }
        }
        private static ReservationService instance { get; set; }
        private ReservationService()
        {
        }
        #endregion

        public List<Reservations> GetReservations(string search)
        {
            using (var context = new RBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Reservations.Where(p => p.Room.RoomName != null && p.Room.RoomName.ToLower()
                        .Contains(search.ToLower()))
                        .OrderBy(x => x.Room.RoomName)
                        .Include(x => x.Room.RoomName)
                        .Include(x=> x.Room.RoomSpecification)
                        .ToList();
                }
                else
                {
                    return context.Reservations
                        .OrderBy(x => x.Room.RoomName)
                        .Include(x => x.Room.RoomName)
                        .Include(x => x.Room.RoomSpecification)
                        .ToList();
                }
            }
        }



     





        public void SaveReservation(Reservations reservations)
        {
            using (var context = new RBContext())
            {
                context.Entry(reservations.Room).State = EntityState.Unchanged;
                context.Entry(reservations.RoomSpecification).State = EntityState.Unchanged;
                context.Reservations.Add(reservations);
                context.SaveChanges();
            }
        }

   

        public Reservations GetReservation(int ID)
        {
            using (var context = new RBContext())
            {
                return context.Reservations.Find(ID);
            }
        }


        public int GetRoomID(int ReservationID)
        {
            try
            {
                using (var context = new RBContext())
                {
                    var roomID = context.Reservations.Where(x => x.ID == ReservationID).Select(x => x.Room.ID).ToArray();
                    return roomID[0];
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }



        //public Reservations GetRoomIDReservation(int ID)
        //{
        //    using (var context = new RBContext())
        //    {
        //        return context.Reservations.Where(x=>x.ID == ID).Select(x=>x.Room.ID);
        //    }
        //}


        public Reservations GetReservationsWithRoom(int ID)
        {
            using (var context = new RBContext())
            {
                return context.Reservations.Include(i => i.Room).Include(i => i.RoomSpecification)
            .FirstOrDefault(x => x.ID == ID);
            }
        }

        public void UpdateReservation(Reservations reservations)
        {
            using (var context = new RBContext())
            {
                context.Entry(reservations).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteReservation(int ID)
        {
            using (var context = new RBContext())
            {

                var reservations = context.Reservations.Find(ID);
                context.Reservations.Remove(reservations);
                context.SaveChanges();
            }
        }



        public void EndMeeting(int ID)
        {
            try
            {
                using (var context = new RBContext())
                {

                    Reservations res = context.Reservations.Single(c => c.ID == ID);
                    res.MeetingStatus = "Completed";
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }





    }
}
