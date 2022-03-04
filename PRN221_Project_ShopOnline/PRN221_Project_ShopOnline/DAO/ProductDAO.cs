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
    }
}
