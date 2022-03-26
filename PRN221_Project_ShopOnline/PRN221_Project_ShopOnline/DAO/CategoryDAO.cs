using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;

namespace PRN221_Project_ShopOnline.DAO
{
    public class CategoryDAO
    {
        private ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();

        public IEnumerable<Category> GetAllCategories()
        {
            context = new ElectronicShopPRN221Context();

            IEnumerable<Category> categories = context.Categories.ToList();
            return categories;
        }
    }
}
