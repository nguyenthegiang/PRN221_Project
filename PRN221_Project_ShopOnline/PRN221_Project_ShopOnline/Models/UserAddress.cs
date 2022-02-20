using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Project_ShopOnline.Models
{
    public partial class UserAddress
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public int? ShipCityId { get; set; }
        public string PhoneNum { get; set; }

        public virtual Ship ShipCity { get; set; }
        public virtual User User { get; set; }
    }
}
