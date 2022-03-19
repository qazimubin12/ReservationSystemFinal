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
    
    public class MasterReservationsService
    {
        #region Singleton
        public static MasterReservationsService Instance
        {
            get
            {
                if (instance == null) instance = new MasterReservationsService();
                return instance;
            }
        }
        private static MasterReservationsService instance { get; set; }
        private MasterReservationsService()
        {
        }
        #endregion

       
        public List<Reservations> GetMasterReservations()
        {
            using (var context = new RBContext())
            {
                var result = context.Reservations.Include(x=> x.Room).Include(x=> x.RoomSpecification).ToList();
                return result;
            }
        }


        public Reservations GetMasterReservation(int ID)
        {
            using (var context = new RBContext())
            {
                return context.Reservations.Include(i => i.Room).Include(i=> i.RoomSpecification)
            .FirstOrDefault(x => x.ID == ID);
            }
        }

        public void UpdateMasterReservation(Reservations reservations)
        {
            try
            {
                using (var context = new RBContext())
                {
                    context.Entry(reservations).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }




        public void UpdateRoomInReservation(int ID,int RID)
        {
            try
            {
                using (var context = new RBContext())
                {

                    Reservations res = context.Reservations.Single(c => c.ID == ID);
                    Room room = context.Rooms.Single(c => c.ID == RID);

                    res.Room = room;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }


        public void DeleteMasterReservation(int ID)
        {
            using (var context = new RBContext())
            {
                var reservations = context.Reservations.Find(ID);
                context.Reservations.Remove(reservations);
                context.SaveChanges();
            }
        }
    }
}
