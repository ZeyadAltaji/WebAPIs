﻿using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccessLayer
{
    public class DBContext : DbContext
    {
        public DBContext (DbContextOptions<DBContext> option) : base(option) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Sub_Slider> SubSliders { get; set; }
        public DbSet<Special> Specials { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brands> Brand { get; set; }
        public DbSet<Vehicles> Vehicle { get; set; }
        public DbSet<PhotoLogo> PhotoLogo { get; set; }
        public DbSet<SubProducts> SProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ItemCart> ItemCart { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Messages> Messages { get; set; }

        public DbSet<Language> Language { get; set; }



    }

}
