using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Project_ShopOnline.Models
{
    public partial class ShipInfo
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public int? ShipCityId { get; set; }
        public string PhoneNum { get; set; }
        public string Note { get; set; }

        public virtual Order Order { get; set; }
        public virtual Ship ShipCity { get; set; }
    }
}
