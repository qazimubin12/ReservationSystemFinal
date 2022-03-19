using ReservationSystem.Database;
using ReservationSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace ReservationSystem.Services
{

    public class RoomSpecificationService
    {

        #region Singleton
        public static RoomSpecificationService Instance
        {
            get
            {
                if (instance == null) instance = new RoomSpecificationService();
                return instance;
            }
        }
        private static RoomSpecificationService instance { get; set; }
        private RoomSpecificationService()
        {
        }
        #endregion

        public List<RoomSpecification> GetRoomSpecifications()
        {
            using (var context = new RBContext())
            {
                return context.RoomSpecifications.ToList();
            }
        }


        public RoomSpecification GetRoomSpecification(int ID)
        {
            using (var context = new RBContext())
            {
                return context.RoomSpecifications.Find(ID);
            }
        }

    


        public void SaveRoomSpecification(RoomSpecification roomSpecification)
        {
            using (var context = new RBContext())
            {
                context.RoomSpecifications.Add(roomSpecification);
                context.SaveChanges();
            }
        }

        public void UpdateRoomSpecification(RoomSpecification roomSpecification)
        {
            using (var context = new RBContext())
            {
                context.Entry(roomSpecification).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteRoomSpecification(int ID)
        {
            using (var context = new RBContext())
            {

                var roomspecification = context.RoomSpecifications.Find(ID);
                context.RoomSpecifications.Remove(roomspecification);
                context.SaveChanges();
            }
        }
    }
}
