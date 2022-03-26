using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Project_ShopOnline.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
            UserAddresses = new HashSet<UserAddress>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? IsSeller { get; set; }
        public int? IsAdmin { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
