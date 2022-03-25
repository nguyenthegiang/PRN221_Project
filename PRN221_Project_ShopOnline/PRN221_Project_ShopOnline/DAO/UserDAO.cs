using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace PRN221_Project_ShopOnline.DAO
{
    public class UserDAO
    {
        private readonly ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();

        /*-------------Customer-------------*/
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

        /*-------------Admin-------------*/
        public IEnumerable<User> GetAllAccounts()
        {
            List<User> users = context.Users.ToList();
            return users;
        }

        public User GetAccountById(int userId)
        {
            User user = context.Users.SingleOrDefault(u => u.UserId == userId);
            return user;
        }

        //return false if not successful
        public bool DeleteAccountById(int userId)
        {
            User user = GetAccountById(userId);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();

                return true;
            }
            else
            {
                //User not exist
                return false;
            }
        }

        //Edit account
        public void EditAccount(User user)
        {
            context.Entry<User>(user).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
