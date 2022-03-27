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
        private ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();

        /*-------------Customer-------------*/
        public User Login(string username, string password)
        {
            context = new ElectronicShopPRN221Context();

            User user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }

        public void SignUp(string username, string password, string email)
        {
            context = new ElectronicShopPRN221Context();

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
            context = new ElectronicShopPRN221Context();

            List<User> users = context.Users.ToList();
            return users;
        }

        public User GetAccountById(int userId)
        {
            context = new ElectronicShopPRN221Context();

            User user = context.Users.SingleOrDefault(u => u.UserId == userId);
            return user;
        }

        //return false if not successful
        public bool DeleteAccountById(int userId)
        {
            context = new ElectronicShopPRN221Context();

            User user = GetAccountById(userId);
            if (user != null)
            {
                /*Before Delete: Delete all this User's Cart, Address, OrderDetail, ShipInfo, Order*/
                //Cart
                CartDAO cartDAO = new CartDAO();
                cartDAO.DeleteCart(userId);
                //UserAddress
                context = new ElectronicShopPRN221Context();
                List<UserAddress> addresses = context.UserAddresses.Where(u => u.UserId == userId).ToList();
                context = new ElectronicShopPRN221Context();
                context.UserAddresses.RemoveRange(addresses);
                context.SaveChanges();
                //Order
                OrderDAO orderDAO = new OrderDAO();
                List<Order> orders = orderDAO.GetOrdersByUser(userId);
                //With each order:
                foreach (Order order in orders)
                {
                    //delete all its OrderDetails
                    List<OrderDetail> orderDetails = orderDAO.GetOrderDetailsByOrder(order.Id);
                    foreach (OrderDetail orderDetail in orderDetails)
                    {
                        orderDAO.DeleteOrderDetail(orderDetail);
                    }

                    //delete all its ShipInfo
                    context = new ElectronicShopPRN221Context();
                    List<ShipInfo> shipInfos = context.ShipInfos.Where(s => s.OrderId == order.Id).ToList();
                    context = new ElectronicShopPRN221Context();
                    context.ShipInfos.RemoveRange(shipInfos);
                    context.SaveChanges();

                    //delete the Order
                    orderDAO.DeleteOrder(order);
                }

                /*Delete User*/
                context = new ElectronicShopPRN221Context();
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
            context = new ElectronicShopPRN221Context();

            context.Entry<User>(user).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
