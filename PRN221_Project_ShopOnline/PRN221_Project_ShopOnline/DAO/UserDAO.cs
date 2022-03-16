using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;

namespace PRN221_Project_ShopOnline.DAO
{
    public class UserDAO
    {
        private readonly ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();

        public User Login(string username, string password)
        {
            User user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }

        public void SignUp(string username, string password, string email)
        {
            User user = new User();
            user.Username = username;
            user.Password = password;
            user.Email = email;
            //default role: customer
            user.IsAdmin = 0;
            user.IsSeller = 0;

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
