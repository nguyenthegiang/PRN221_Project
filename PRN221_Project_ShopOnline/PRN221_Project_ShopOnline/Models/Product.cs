using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Project_ShopOnline.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int? OriginalPrice { get; set; }
        public int? SellPrice { get; set; }
        public int? SalePercent { get; set; }
        public string ImageLink { get; set; }
        public int? CategoryId { get; set; }
        public int? SellerId { get; set; }
        public int? Amount { get; set; }
        public int? StatusId { get; set; }
        public int? ManufacturerId { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Weight { get; set; }

        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual User Seller { get; set; }
        public virtual ProductStatus Status { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        /*Function to write the Product's Price in proper form:
        Example: 1.000.000*/
        public String GetPriceWithDot()
        {
            int price = (int)SellPrice;
            return price.ToString("N0");
        }
    }
}
