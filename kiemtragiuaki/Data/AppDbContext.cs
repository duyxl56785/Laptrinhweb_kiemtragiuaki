using kiemtragiuaki.Models;
using Microsoft.EntityFrameworkCore;

namespace kiemtragiuaki.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DishCategory_BIT240081> DishCategories => Set<DishCategory_BIT240081>();
        public DbSet<Dish_BIT240081> Dishes => Set<Dish_BIT240081>();
        public DbSet<DishImage_BIT240081> DishImages => Set<DishImage_BIT240081>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishCategory_BIT240081>(e =>
            {
                e.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Dish_BIT240081>(e =>
            {
                // A category that still has dishes cannot be deleted.
                e.HasOne(d => d.DishCategory)
                    .WithMany(c => c.Dishes)
                    .HasForeignKey(d => d.DishCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Dish names are unique within one category.
                e.HasIndex(d => new { d.DishCategoryId, d.Name }).IsUnique();

                e.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_BIT240081_Dishes_Price", "\"BIT240081_Price\" > 0");
                    t.HasCheckConstraint("CK_BIT240081_Dishes_PreparationTime", "\"BIT240081_PreparationTime\" > 0");
                });
            });

            modelBuilder.Entity<DishImage_BIT240081>(e =>
            {
                e.HasOne(i => i.Dish)
                    .WithMany(d => d.Images)
                    .HasForeignKey(i => i.DishId)
                    .OnDelete(DeleteBehavior.Cascade);

                // At most one thumbnail per dish.
                e.HasIndex(i => i.DishId)
                    .IsUnique()
                    .HasFilter("\"BIT240081_IsThumbnail\" = true")
                    .HasDatabaseName("IX_BIT240081_DishImages_OneThumbnail");
            });

            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder mb)
        {
            mb.Entity<DishCategory_BIT240081>().HasData(
                new DishCategory_BIT240081 { Id = 1, Name = "Món chính", Description = "Các món ăn chính" },
                new DishCategory_BIT240081 { Id = 2, Name = "Khai vị", Description = "Món khai vị" },
                new DishCategory_BIT240081 { Id = 3, Name = "Tráng miệng", Description = "Món tráng miệng" });

            mb.Entity<Dish_BIT240081>().HasData(
                new Dish_BIT240081 { Id = 1, Name = "Phở bò", Price = 55000, PreparationTime = 20, IsAvailable = true, Description = "Phở bò tái chín truyền thống", DishCategoryId = 1 },
                new Dish_BIT240081 { Id = 2, Name = "Cơm tấm sườn", Price = 45000, PreparationTime = 15, IsAvailable = true, Description = "Cơm tấm sườn nướng, bì, chả", DishCategoryId = 1 },
                new Dish_BIT240081 { Id = 3, Name = "Bún chả", Price = 50000, PreparationTime = 25, IsAvailable = false, Description = "Bún chả Hà Nội", DishCategoryId = 1 },
                new Dish_BIT240081 { Id = 4, Name = "Gỏi cuốn", Price = 30000, PreparationTime = 10, IsAvailable = true, Description = "Gỏi cuốn tôm thịt", DishCategoryId = 2 },
                new Dish_BIT240081 { Id = 5, Name = "Chả giò", Price = 35000, PreparationTime = 18, IsAvailable = true, Description = "Chả giò chiên giòn", DishCategoryId = 2 },
                new Dish_BIT240081 { Id = 6, Name = "Chè ba màu", Price = 20000, PreparationTime = 5, IsAvailable = true, Description = "Chè đậu, thạch, nước cốt dừa", DishCategoryId = 3 },
                new Dish_BIT240081 { Id = 7, Name = "Bánh flan", Price = 15000, PreparationTime = 30, IsAvailable = false, Description = "Bánh flan caramen", DishCategoryId = 3 });

            // Photos from Wikimedia Commons. "Chè ba màu" (Id 6) intentionally has no image
            // to demonstrate the "dish without images" case.
            const string wm = "https://upload.wikimedia.org/wikipedia/commons/";
            mb.Entity<DishImage_BIT240081>().HasData(
                new DishImage_BIT240081 { Id = 1, DishId = 1, ImageUrl = wm + "thumb/9/99/Ph%E1%BB%9F_b%C3%B2%2C_C%E1%BA%A7u_Gi%E1%BA%A5y%2C_H%C3%A0_N%E1%BB%99i.jpg/960px-Ph%E1%BB%9F_b%C3%B2%2C_C%E1%BA%A7u_Gi%E1%BA%A5y%2C_H%C3%A0_N%E1%BB%99i.jpg", IsThumbnail = true },
                new DishImage_BIT240081 { Id = 2, DishId = 1, ImageUrl = wm + "thumb/4/45/Pho_Bo_by_Banh_%26_Mee_in_Kirkgate_Market.jpg/960px-Pho_Bo_by_Banh_%26_Mee_in_Kirkgate_Market.jpg", IsThumbnail = false },
                new DishImage_BIT240081 { Id = 3, DishId = 2, ImageUrl = wm + "thumb/b/b0/C%C6%A1m_T%E1%BA%A5m%2C_Da_Nang%2C_Vietnam.jpg/960px-C%C6%A1m_T%E1%BA%A5m%2C_Da_Nang%2C_Vietnam.jpg", IsThumbnail = true },
                new DishImage_BIT240081 { Id = 4, DishId = 4, ImageUrl = wm + "thumb/0/03/Summer_roll.jpg/960px-Summer_roll.jpg", IsThumbnail = true },
                new DishImage_BIT240081 { Id = 5, DishId = 4, ImageUrl = wm + "thumb/9/90/G%E1%BB%8Fi_cu%E1%BB%91n.jpg/960px-G%E1%BB%8Fi_cu%E1%BB%91n.jpg", IsThumbnail = false },
                new DishImage_BIT240081 { Id = 101, DishId = 2, ImageUrl = wm + "thumb/4/40/C%C6%A1m_t%E1%BA%A5m_s%C6%B0%E1%BB%9Dn_c%C3%A2y.JPG/960px-C%C6%A1m_t%E1%BA%A5m_s%C6%B0%E1%BB%9Dn_c%C3%A2y.JPG", IsThumbnail = false },
                new DishImage_BIT240081 { Id = 102, DishId = 3, ImageUrl = wm + "thumb/4/4d/B%C3%BAn_ch%E1%BA%A3_Vietnamese_food.jpg/960px-B%C3%BAn_ch%E1%BA%A3_Vietnamese_food.jpg", IsThumbnail = true },
                new DishImage_BIT240081 { Id = 103, DishId = 3, ImageUrl = wm + "thumb/7/7f/Bun-cha-hanoi.jpg/960px-Bun-cha-hanoi.jpg", IsThumbnail = false },
                new DishImage_BIT240081 { Id = 104, DishId = 5, ImageUrl = wm + "6/6b/Cha_gio.jpg", IsThumbnail = true },
                new DishImage_BIT240081 { Id = 105, DishId = 5, ImageUrl = wm + "thumb/b/bb/Ch%E1%BA%A3_gi%C3%B2_%28Vietnamese_Spring_Rolls%29_-_12.jpg/960px-Ch%E1%BA%A3_gi%C3%B2_%28Vietnamese_Spring_Rolls%29_-_12.jpg", IsThumbnail = false },
                new DishImage_BIT240081 { Id = 106, DishId = 7, ImageUrl = wm + "thumb/4/43/Homemade_Flan.jpg/960px-Homemade_Flan.jpg", IsThumbnail = true },
                new DishImage_BIT240081 { Id = 107, DishId = 7, ImageUrl = wm + "thumb/6/64/Cr%C3%A8me_caramel_2.jpg/960px-Cr%C3%A8me_caramel_2.jpg", IsThumbnail = false });
        }
    }
}
