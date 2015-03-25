using AvansFestivals.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Abstract
{
    public interface IUserRepo
    {
        User GetUser(string username);
        bool CheckLogin(string username, string password);
        bool IsInRole(User user, string role);
        bool CreateUser(User user);
        User EditUser();
    }
}
