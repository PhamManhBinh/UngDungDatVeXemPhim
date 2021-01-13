using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CINEMA.EF;

namespace CINEMA.Models
{
    public class UserDAO
    {
        CinemaDbContext db = null;
        public UserDAO()
        {
            db = new CinemaDbContext();
        }
        public bool CheckUser(string Email)
        {
            return db.Users.Count(x => x.Email == Email) > 0;
        }
        public int InsertUser(User User)
        {
            db.Users.Add(User);
            db.SaveChanges();
            return User.id;
        }
        public void UpdateUser(User User)
        {
            db.Entry(User).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public bool Login(LoginModel loginModel)
        {
            return db.Users.Count(x => x.Email == loginModel.Email && x.Password == loginModel.Password) > 0;
        }
        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }
        public User GetByID(string id)
        {
            return db.Users.Find(id);
        }
    }
}