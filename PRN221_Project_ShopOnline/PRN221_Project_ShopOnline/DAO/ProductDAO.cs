using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace PRN221_Project_ShopOnline.DAO
{
    public class ProductDAO
    {
        private ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();

        /*-------------Customer-------------*/
        public IEnumerable<Product> GetAllProducts()
        {
            context = new ElectronicShopPRN221Context();

            IEnumerable<Product> products = context.Products.ToList();
            return products;
        }

        public Product GetProductByID(int Id)
        {
            context = new ElectronicShopPRN221Context();

            Product product = context.Products.SingleOrDefault(p => p.ProductId == Id);
            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(int CateId)
        {
            context = new ElectronicShopPRN221Context();

            IEnumerable<Product> products = context.Products.Where(p => p.CategoryId == CateId).ToList();
            return products;
        }

        public IEnumerable<Product> SearchProductByName(string productName)
        {
            context = new ElectronicShopPRN221Context();

            IEnumerable<Product> products = context.Products.Where(p => p.ProductName.Contains(productName)).ToList();
            return products;
        }

        /*-------------Seller-------------*/
        public IEnumerable<Product> GetProductsBySeller(int SellerId)
        {
            context = new ElectronicShopPRN221Context();

            IEnumerable<Product> products = context.Products.Where(p => p.SellerId == SellerId).ToList();
            return products;
        }

        //Add 1 Product to DB, return if it is successful or not
        public bool AddProduct(Product product)
        {
            context = new ElectronicShopPRN221Context();

            context.Products.Add(product);

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

        //Edit Product
        public void EditProduct(Product product)
        {
            context = new ElectronicShopPRN221Context();

            context.Entry<Product>(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        //Delete Product: return false if unsuccessful
        public bool DeleteProductById(int productId)
        {
            context = new ElectronicShopPRN221Context();

            Product product = GetProductByID(productId);
            if (product != null)
            {
                //Before remove: remove all orderDetails & Carts of this Product
                CartDAO cartDAO = new CartDAO();
                cartDAO.DeleteCartsOfProduct(productId);
                OrderDAO orderDAO = new OrderDAO();
                orderDAO.DeleteOrderDetailsByProduct(productId);

                //Remove Product
                context.Products.Remove(product);
                context.SaveChanges();

                return true;
            }
            else
            {
                //Product not exist
                return false;
            }
        }

        /*-------------For [CartDAO]-------------*/
        //Count the amount of a product to see if it is out of stock: used for AddToCart()
        public int CountAmountOfProduct(int ProductId)
        {
            context = new ElectronicShopPRN221Context();

            Product product = context.Products.SingleOrDefault(p => p.ProductId == ProductId);
            return (int)product.Amount;
        }

        //Minus Amount from Product (used after 1 Customer Add to Cart)
        public void DeleteAmountOfProduct(int ProductId, int Amount)
        {
            //Find Product
            Product product = GetProductByID(ProductId);

            //Update Product Amount
            product.Amount -= Amount;
            EditProduct(product);
        }
    }
}
