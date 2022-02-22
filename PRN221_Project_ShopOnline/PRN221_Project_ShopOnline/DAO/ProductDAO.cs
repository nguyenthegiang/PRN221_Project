using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;

namespace PRN221_Project_ShopOnline.DAO
{
    public class ProductDAO
    {
        public IEnumerable<Product> GetAllroducts()
        {
            ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();
            IEnumerable<Product> products = context.Products.ToList();
            return products;
        }
    }
}
