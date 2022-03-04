using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;

namespace PRN221_Project_ShopOnline.DAO
{
    public class ProductDAO
    {
        private readonly ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();

        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> products = context.Products.ToList();
            return products;
        }

        public Product GetProductByID(int id)
        {
            Product product = context.Products.SingleOrDefault(p => p.ProductId == id);
            return product;
        }
    }
}
