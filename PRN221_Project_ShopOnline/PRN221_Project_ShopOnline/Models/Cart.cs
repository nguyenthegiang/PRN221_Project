using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Project_ShopOnline.Models
{
    public partial class Cart
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int? Amount { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
