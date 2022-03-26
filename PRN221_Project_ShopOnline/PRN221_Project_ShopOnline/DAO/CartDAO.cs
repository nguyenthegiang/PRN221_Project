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
            context = new ElectronicShopPRN221Context();

            List<Cart> carts = context.Carts.Where(c => c.UserId == UserId).ToList();
            return carts;
        }

        /*Function returns a boolean to inform whether the product is added to cart or not
        (product out of stock -> add fail)*/
        public bool AddToCart(int UserId, int ProductId, int Amount)
        {
            //Check if Product is out of stock, or is not enough for this addition
            ProductDAO productDAO = new ProductDAO();
            if (productDAO.CountAmountOfProduct(ProductId) < Amount)
            {
                return false;
            }

            //Check if already has this product in cart -> only increase amount
            List<Cart> carts = GetCartsOfUser(UserId);
            foreach(Cart cart in carts)
            {
                if (cart.Product.ProductId == ProductId)
                {
                    //increase amount
                    context = new ElectronicShopPRN221Context();
                    Cart updateCart = context.Carts.
                        SingleOrDefault(c => c.ProductId == cart.ProductId && c.UserId == cart.UserId); //find this cart
                    updateCart.Amount += Amount;    //update
                    context.SaveChanges();

                    //delete equivalent Amount in Product (stock)
                    productDAO.DeleteAmountOfProduct(ProductId, Amount);
                    return true;
                }
            }

            //If Product is not yet in Cart -> Add new Product to Cart
            Cart newCart = new Cart();
            newCart.UserId = UserId;
            newCart.ProductId = ProductId;
            newCart.Amount = Amount;

            context = new ElectronicShopPRN221Context();
            context.Carts.Add(newCart);
            //check if [Add] is successful or not
            int result = context.SaveChanges();
            if (result < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
