using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace PRN221_Project_ShopOnline.DAO
{
    public class CartDAO
    {
        private ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();

        /*-------------Customer-------------*/
        //Get all Cart Items of 1 User
        public List<Cart> GetCartsOfUser(int UserId)
        {
            List<Cart> carts = context.Carts.Where(c => c.UserId == UserId).ToList();
            return carts;
        }

        /*Function returns a boolean to inform whether the product is added to cart or not
        (product out of stock -> add fail)*/
        public bool AddToCart(int UserId, int ProductId, int Amount)
        {

        }
    }
}
