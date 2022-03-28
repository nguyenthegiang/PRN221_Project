using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN221_Project_ShopOnline.Models
{
    //To store a Product to show in Dashboard (with Proportion)
    public class DashboardProduct
    {
        public string name { get; set; }
        public int amount { get; set; }
        public int proportion { get; set; }

        public DashboardProduct(string name, int amount, int proportion)
        {
            this.name = name;
            this.amount = amount;
            this.proportion = proportion;
        }

        public DashboardProduct()
        {
        }
    }
}
