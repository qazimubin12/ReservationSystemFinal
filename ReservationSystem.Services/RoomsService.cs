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
    public class RoomsService
    {
        #region Singleton
        public static RoomsService Instance
        {
            get
            {
                if (instance == null) instance = new RoomsService();
                return instance;
            }
        }
        private static RoomsService instance { get; set; }
        private RoomsService()
        {
        }
        #endregion

       

        public List<Room> GetRooms(string search)
        {
            using (var context = new RBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Rooms.Where(p => p.RoomName != null && p.RoomName.ToLower()
                        .Contains(search.ToLower()))
                        .OrderBy(x => x.RoomName)
                        .Include(x => x.RoomSpecification)
                        .ToList();
                }
                else
                {
                    return context.Rooms
                        .OrderBy(x => x.RoomName)
                        .Include(x => x.RoomSpecification)
                        .ToList();
                }
            }
        }

        public List<Room> GetAllRooms()
        {
            using (var context = new RBContext())
            {

                return context.Rooms
                    .OrderBy(x => x.RoomName)
                    .Include(x => x.RoomSpecification)
                    .ToList();

            }
        }


        public List<Room> GetAvailableRooms()
        {
            using(var context = new RBContext())
            {
                return context.Rooms.Where(x => x.Vacant == true).Include(x=> x.RoomSpecification).ToList();
            }
        }


     


        public string GetRoomName(int ID)
        {
            using (var context = new RBContext())
            {
                string roomName = (from x in context.Rooms where x.ID == ID select x.RoomName).FirstOrDefault();
                return roomName;
            }
        }



   


        public List<Room> GetNonVacantRooms()
        {
            using (var context = new RBContext())
            {
                return context.Rooms.Where(x => x.Vacant == false).Include(x => x.RoomSpecification).ToList();
            }
        }



        public Room GetRoom(int ID)
        {
            using (var context = new RBContext())
            {
                return context.Rooms.Find(ID);
            }
        }


        public Room GetRoomWithSpecs(int ID)
        {
            using (var context = new RBContext())
            {
                return context.Rooms.Include(i => i.RoomSpecification)
              .FirstOrDefault(x => x.ID == ID);
            }
        }



        public int GetRoomSpecs(int RoomID)
        {
            try
            {
                using (var context = new RBContext())
                {
                    var specsid = context.Rooms.Where(x => x.ID == RoomID).Select(x => x.RoomSpecification.ID).ToArray();
                    return specsid[0];
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public void SaveRoom(Room room)
        {
            using (var context = new RBContext())
            {
                context.Entry(room.RoomSpecification).State = EntityState.Unchanged;
                context.Rooms.Add(room);
                context.SaveChanges();
            }
        }

        public void UpdateRoom(Room room)
        {
            using (var context = new RBContext())
            {
                context.Entry(room).State = EntityState.Modified;
                context.SaveChanges();
            }
        }



   


        public void UpdateVacantByandVacany(int ID, string VacantBy)
        {
            using (var context = new RBContext())
            {
                var vacantroom = context.Rooms.Where(c => c.ID == ID).ToList();
                vacantroom.ForEach(c => c.VacantBy = VacantBy);
                vacantroom.ForEach(c => c.Vacant = false);
                context.SaveChanges();
            }
        }

        public void RelaseUpdateVacantByandVacany(int ID, string VacantBy = "--")
        {
            using (var context = new RBContext())
            {
                var vacantroom = context.Rooms.Where(c => c.ID == ID).ToList();
                vacantroom.ForEach(c => c.VacantBy = VacantBy);
                vacantroom.ForEach(c => c.Vacant = true);
                context.SaveChanges();
            }
        }

        public void DeleteRoom(int ID)
        {
            using (var context = new RBContext())
            {

                var room = context.Rooms.Find(ID);
                context.Rooms.Remove(room);
                context.SaveChanges();
            }
        }
    }
}
