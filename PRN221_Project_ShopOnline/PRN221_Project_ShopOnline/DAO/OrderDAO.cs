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
    }
}
