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
    public class UsersService
    {
        #region Singleton
        public static UsersService Instance
        {
            get
            {
                if (instance == null) instance = new UsersService();
                return instance;
            }
        }
        private static UsersService instance { get; set; }
        private UsersService()
        {
        }
        #endregion
        public Users GetUser(int ID)
        {
            using (var context = new RBContext())
            {
                return context.Users.Find(ID);
            }
        }



        public List<Users> GetUsers()
        {
            using (var context = new RBContext())
            {
                return context.Users.ToList();
            }
        }

        public void SaveUser(Users user)
        {
            using (var context = new RBContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void UpdateUser(Users user)
        {
            using (var context = new RBContext())
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteUser(int ID)
        {
            using (var context = new RBContext())
            {
                var user = context.Users.Find(ID);
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

    }
}
