using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace PRN221_Project_ShopOnline.DAO
{
    public class OrderDAO
    {
        private ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();

        /*-------------Seller-------------*/
        //Used for Delete Product (DAO)
        public void DeleteOrderDetailsByProduct(int ProductId)
        {
            //Get OrderDetails of this Product
            context = new ElectronicShopPRN221Context();
            List<OrderDetail> orderDetails = context.OrderDetails.Where(o => o.ProductId == ProductId).ToList();

            //Delete
            context = new ElectronicShopPRN221Context();
            context.OrderDetails.RemoveRange(orderDetails);
            context.SaveChanges();
        }

        /*-------------Admin-------------*/
        //Used for Delete User
        public List<Order> GetOrdersByUser(int UserId)
        {
            //Get orders of this user
            context = new ElectronicShopPRN221Context();

            List<Order> orders = context.Orders.Where(o => o.UserId == UserId).ToList();

            return orders;
        }

        //Used for Delete User
        public List<OrderDetail> GetOrderDetailsByOrder(int OrderId)
        {
            context = new ElectronicShopPRN221Context();
            List<OrderDetail> orderDetails = context.OrderDetails.Where(o => o.OrderId == OrderId).ToList();
            return orderDetails;
        }

        //Used for Delete User
        public void DeleteOrder(Order order)
        {
            context = new ElectronicShopPRN221Context();
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        //Used for Delete User
        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            context = new ElectronicShopPRN221Context();
            context.OrderDetails.Remove(orderDetail);
            context.SaveChanges();
        }
    }
}
