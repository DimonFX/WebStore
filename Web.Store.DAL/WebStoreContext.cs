﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Entities.Base;
namespace Web.Store.DAL
{
    public class WebStoreContext:IdentityDbContext<User>
    {
        public WebStoreContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
