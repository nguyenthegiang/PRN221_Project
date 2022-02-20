using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Project_ShopOnline.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ShipInfos = new HashSet<ShipInfo>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public double? TotalPrice { get; set; }
        public string Note { get; set; }
        public int? Status { get; set; }
        public DateTime? DayBuy { get; set; }

        public virtual OrderStatus StatusNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ShipInfo> ShipInfos { get; set; }
    }
}
