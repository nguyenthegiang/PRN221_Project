using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Project_ShopOnline.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        //For Display in CSHTML with HTMLHelper
        public override string ToString()
        {
            return CategoryName;
        }
    }
}
