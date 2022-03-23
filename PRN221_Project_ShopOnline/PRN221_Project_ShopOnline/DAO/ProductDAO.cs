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
        private readonly ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();

        /*-------------Customer-------------*/
        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> products = context.Products.ToList();
            return products;
        }

        public Product GetProductByID(int Id)
        {
            Product product = context.Products.SingleOrDefault(p => p.ProductId == Id);
            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(int CateId)
        {
            IEnumerable<Product> products = context.Products.Where(p => p.CategoryId == CateId).ToList();
            return products;
        }

        public IEnumerable<Product> SearchProductByName(string productName)
        {
            IEnumerable<Product> products = context.Products.Where(p => p.ProductName.Contains(productName)).ToList();
            return products;
        }

        /*-------------Seller-------------*/
        public IEnumerable<Product> GetProductsBySeller(int SellerId)
        {
            IEnumerable<Product> products = context.Products.Where(p => p.SellerId == SellerId).ToList();
            return products;
        }

        //Add 1 Product to DB, return if it is successful or not
        public bool AddProduct(Product product)
        {
            context.Products.Add(product);
            
            //check if [Add] is successful or not
            int result = context.SaveChanges();
            if (result < 1)
            {
                return false;
            } else
            {
                return true;
            }
        }

        //Edit Product, return if it is successful or not
        public bool EditProduct(Product product)
        {
            context.Entry<Product>(product).State = EntityState.Modified;
            context.SaveChanges();

            //check if [Edit] is successful or not
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
