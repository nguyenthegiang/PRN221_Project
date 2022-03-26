using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace PRN221_Project_ShopOnline.Models
{
    public partial class ElectronicShopPRN221Context : DbContext
    {
        public ElectronicShopPRN221Context()
        {
        }

        public ElectronicShopPRN221Context(DbContextOptions<ElectronicShopPRN221Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductStatus> ProductStatuses { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }
        public virtual DbSet<ShipInfo> ShipInfos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Read from Configuration file
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("PRN221"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId });

                entity.ToTable("Cart");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productID_in_product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userID_in_user");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(1000);

                entity.Property(e => e.Icon)
                    .HasMaxLength(1000)
                    .HasColumnName("icon");
            });

            modelBuilder.Entity<Information>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Fax).HasColumnName("fax");

                entity.Property(e => e.Phone).HasColumnName("phone");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("Manufacturer");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.ManufacturerName).HasMaxLength(1000);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DayBuy).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("statusID_in_order_status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("userID_in_user_order");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("Order_Detail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderID_in_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productID_in_order_detail");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("Order_Status");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.ImageLink)
                    .HasMaxLength(1000)
                    .HasColumnName("imageLink");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.ProductName).HasMaxLength(1000);

                entity.Property(e => e.SellerId).HasColumnName("SellerID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.Property(e => e.Width).HasColumnName("width");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("product_in_category");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("ManufacturerID_in_Manufacturer");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("SellerID_in_Users");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("StatusID_in_Status");
            });

            modelBuilder.Entity<ProductStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__ProductS__C8EE2043BFCEF0DD");

                entity.ToTable("ProductStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusName).HasMaxLength(1000);
            });

            modelBuilder.Entity<Ship>(entity =>
            {
                entity.ToTable("Ship");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityName).HasMaxLength(1000);
            });

            modelBuilder.Entity<ShipInfo>(entity =>
            {
                entity.ToTable("ShipInfo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerName).HasMaxLength(1000);

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.PhoneNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShipCityId).HasColumnName("ShipCityID");

                entity.Property(e => e.ShippingAddress).HasMaxLength(1000);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ShipInfos)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("order_id_in_order");

                entity.HasOne(d => d.ShipCity)
                    .WithMany(p => p.ShipInfos)
                    .HasForeignKey(d => d.ShipCityId)
                    .HasConstraintName("ship_city_in_ship_info");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .HasMaxLength(1000)
                    .HasColumnName("email");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.IsSeller).HasColumnName("isSeller");

                entity.Property(e => e.Password).HasMaxLength(1000);

                entity.Property(e => e.Username).HasMaxLength(500);
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.ToTable("UserAddress");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PhoneNum)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ShipAddress).HasMaxLength(1000);

                entity.Property(e => e.ShipCityId).HasColumnName("ShipCityID");

                entity.Property(e => e.ShipName).HasMaxLength(500);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ShipCity)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.ShipCityId)
                    .HasConstraintName("ship_city_in_ship_address");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("userID_in_user_address");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
