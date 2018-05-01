using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using TungShop.Model.Models;

namespace TungShop.Data
{
    public class TungShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public TungShopDbContext() : base("TungShopConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Footer> Footers { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuGroup> MenuGroups { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<PostTag> PostTags { set; get; }
        public DbSet<Product> Products { set; get; }

        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<SupportOnline> SupportOnlines { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }

        public DbSet<Tag> Tags { set; get; }
        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<Student> Students { set; get; }//sinh vien
        public DbSet<Room> Rooms { set; get; } //phong
        public DbSet<ListAsset> ListAssets { set; get; } //danh muc tai san
        public DbSet<RoomAsset> RoomAssets { set; get; } //tai san phong
        public DbSet<Contract> Contracts { set; get; } // hop dong
        public DbSet<Invoice> Invoices { set; get; }// hoa don
        public DbSet<ElectricityWater> ElectricityWaters { set; get; }//dien nuoc
        public DbSet<ElectricityWaterHistory> ElectricityWaterHistorys { set; get; }//lich su dien nuoc

        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }
        public DbSet<Approval> Approvals { set; get; }

        public static TungShopDbContext Create()
        {
            return new TungShopDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder) // ghi đè phuong thức khởi tạo DB của DbContext lúc khỏi tạo entity framework
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().HasKey(r=>r.Id).ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().ToTable("ApplicationUserClaims");

        }
    }
}
