using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TungShop.Model.Models;

namespace TungShop.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TungShop.Data.TungShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TungShop.Data.TungShopDbContext context)
        {
            CreateProductCategorySample(context);
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TungShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TungShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Admin"

            };                                                 

            manager.Create(user, "@123456");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("admin@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

        }

        private void CreateProductCategorySample(TungShop.Data.TungShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name="Điện lạnh",Alias="dien-lanh",Status=true },
                    new ProductCategory() { Name="Viễn thông",Alias="vien-thong",Status=true },
                    new ProductCategory() { Name="Đồ gia dụng",Alias="do-gia-dung",Status=true },
                    new ProductCategory() { Name="Mỹ phẩm",Alias="my-pham",Status=true }
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }

        }
    }
}
