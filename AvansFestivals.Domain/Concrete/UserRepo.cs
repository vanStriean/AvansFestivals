using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Concrete
{
    public class UserRepo : IUserRepo
    {
        AvansFestivalsEntities db = new AvansFestivalsEntities();
        public User GetUser(string username)
        {
            return db.Users.Where(w => w.Username.ToLower() == username.ToLower()).FirstOrDefault();
        }

        public bool CheckLogin(string username, string password)
        {
            User user = db.Users.Where(w => w.Username.ToLower() == username.ToLower()).FirstOrDefault();
            return user.Password == password;
        }

        public bool IsInRole(User user, string role)
        {
            return db.Users.Find(user).Role == role;
        }

        public bool CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return true;
        }

        public User EditUser()
        {
            throw new NotImplementedException();
        }
    }
}
