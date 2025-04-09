using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UsersMessage> UsersMessages { get; set; }
        public DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Otp> Otps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<User>()
                .OwnsMany(u => u.RefreshTokens);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Review>().HasQueryFilter(r => !r.IsDeleted);
            // Indexes
            builder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.UserName).IsUnique();
                entity.HasIndex(u => u.PhoneNumber).IsUnique();
                entity.HasIndex(u => u.IsDeleted);
                entity.HasQueryFilter(u => !u.IsDeleted);
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasIndex(p => p.Name);
                entity.HasIndex(p => p.SKU).IsUnique();
                entity.HasIndex(p => p.Price);
                entity.HasIndex(p => p.IsDeleted);
                entity.HasQueryFilter(p => !p.IsDeleted);
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId);
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Name);
                entity.HasIndex(c => c.ParentCategoryId);
                entity.HasIndex(c => c.IsDeleted);
                entity.HasQueryFilter(c => !c.IsDeleted);
                entity.HasMany(c => c.SubCategories)
                  .WithOne(c => c.ParentCategory)
                  .HasForeignKey(c => c.ParentCategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Order>(entity =>
            {
                entity.HasIndex(o => o.UserId);
                entity.HasIndex(o => o.OrderDate);
                entity.HasIndex(o => o.Status);
            });

            builder.Entity<Cart>(entity =>
            {
                entity.HasIndex(c => c.UserId).IsUnique();
            });

            builder.Entity<CartItem>(entity =>
            {
                entity.HasIndex(ci => ci.ProductId);
            });
            builder.Entity<Review>(entity =>
            {
                entity.HasOne(r => r.User)
                      .WithMany()
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Product)
                      .WithMany()
                      .HasForeignKey(r => r.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Wishlist>(entity =>
            {
                entity.HasOne(w => w.User)
                      .WithOne(u => u.Wishlist)
                      .HasForeignKey<Wishlist>(w => w.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<WishlistItem>(entity =>
            {
                entity.HasIndex(wi => wi.ProductId);
            });

            builder.Entity<Address>(entity =>
            {
                entity.HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);
            });

            builder.Entity<City>()
                .HasOne(c => c.Governorate)
                .WithMany(g => g.Cities)
                .HasForeignKey(c => c.GovernorateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<NewsletterSubscriber>()
                .HasIndex(n => n.Email)
                .IsUnique();

            builder.Entity<BlogPost>()
                .HasQueryFilter(p => !p.IsDeleted);

            builder.Entity<Otp>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OtpCode).HasMaxLength(6);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Token).IsRequired();
                entity.Property(e => e.ExpirationDateTime).IsRequired();

                entity.HasIndex(e => new { e.Email, e.OtpCode });
            });

            seedRoles(builder);
            addUser(builder);
            assignRole(builder);

            var alexId = new Guid("01111111-1111-1111-1111-111111111111");
            var behiraId = new Guid("02111111-1111-1111-1111-111111111111");
            var kafrElSheikhId = new Guid("03111111-1111-1111-1111-111111111111");
            var gharbiaId = new Guid("04111111-1111-1111-1111-111111111111");
            var menoufiaId = new Guid("05111111-1111-1111-1111-111111111111");
            var dakahliaId = new Guid("06111111-1111-1111-1111-111111111111");
            var sharqiaId = new Guid("07111111-1111-1111-1111-111111111111");
            var damiettaId = new Guid("08111111-1111-1111-1111-111111111111");
            var qalyubiaId = new Guid("09111111-1111-1111-1111-111111111111");
            var cairoId = new Guid("10111111-1111-1111-1111-111111111111");

            builder.Entity<Governorate>().HasData(
                new Governorate { Id = alexId, Name = "الإسكندرية" },
                new Governorate { Id = behiraId, Name = "البحيرة" },
                new Governorate { Id = kafrElSheikhId, Name = "كفر الشيخ" },
                new Governorate { Id = gharbiaId, Name = "الغربية" },
                new Governorate { Id = menoufiaId, Name = "المنوفية" },
                new Governorate { Id = dakahliaId, Name = "الدقهلية" },
                new Governorate { Id = sharqiaId, Name = "الشرقية" },
                new Governorate { Id = damiettaId, Name = "دمياط" },
                new Governorate { Id = qalyubiaId, Name = "القليوبية" },
                new Governorate { Id = cairoId, Name = "القاهرة" }
            );

            builder.Entity<City>().HasData(
                // الإسكندرية
                new City { Id = new Guid("11111111-1111-1111-1111-111111111112"), Name = "الإسكندرية", GovernorateId = alexId },
                new City { Id = new Guid("11111111-1111-1111-1111-111111111113"), Name = "برج العرب", GovernorateId = alexId },

                // البحيرة
                new City { Id = new Guid("21111111-1111-1111-1111-211111111112"), Name = "دمنهور", GovernorateId = behiraId },
                new City { Id = new Guid("21111111-1111-1111-1111-211111111113"), Name = "رشيد", GovernorateId = behiraId },
                new City { Id = new Guid("21111111-1111-1111-1111-211111111114"), Name = "كفر الدوار", GovernorateId = behiraId },
                new City { Id = new Guid("21111111-1111-1111-1111-211111111115"), Name = "إدكو", GovernorateId = behiraId },

                // كفر الشيخ
                new City { Id = new Guid("31111111-1111-1111-1111-311111111112"), Name = "كفر الشيخ", GovernorateId = kafrElSheikhId },
                new City { Id = new Guid("31111111-1111-1111-1111-311111111113"), Name = "دسوق", GovernorateId = kafrElSheikhId },
                new City { Id = new Guid("31111111-1111-1111-1111-311111111114"), Name = "بيلا", GovernorateId = kafrElSheikhId },
                new City { Id = new Guid("31111111-1111-1111-1111-311111111115"), Name = "فوه", GovernorateId = kafrElSheikhId },

                // الغربية
                new City { Id = new Guid("41111111-1111-1111-1111-411111111112"), Name = "طنطا", GovernorateId = gharbiaId },
                new City { Id = new Guid("41111111-1111-1111-1111-411111111113"), Name = "المحلة الكبرى", GovernorateId = gharbiaId },
                new City { Id = new Guid("41111111-1111-1111-1111-411111111114"), Name = "كفر الزيات", GovernorateId = gharbiaId },
                new City { Id = new Guid("41111111-1111-1111-1111-411111111115"), Name = "سمنود", GovernorateId = gharbiaId },
                new City { Id = new Guid("41111111-1111-1111-1111-411111111116"), Name = "زفتى", GovernorateId = gharbiaId },

                // المنوفية
                new City { Id = new Guid("51111111-1111-1111-1111-511111111112"), Name = "شبين الكوم", GovernorateId = menoufiaId },
                new City { Id = new Guid("51111111-1111-1111-1111-511111111113"), Name = "قويسنا", GovernorateId = menoufiaId },
                new City { Id = new Guid("51111111-1111-1111-1111-511111111114"), Name = "منوف", GovernorateId = menoufiaId },
                new City { Id = new Guid("51111111-1111-1111-1111-511111111115"), Name = "تلا", GovernorateId = menoufiaId },

                // الدقهلية
                new City { Id = new Guid("61111111-1111-1111-1111-611111111112"), Name = "المنصورة", GovernorateId = dakahliaId },
                new City { Id = new Guid("61111111-1111-1111-1111-611111111113"), Name = "ميت غمر", GovernorateId = dakahliaId },
                new City { Id = new Guid("61111111-1111-1111-1111-611111111114"), Name = "أجا", GovernorateId = dakahliaId },
                new City { Id = new Guid("61111111-1111-1111-1111-611111111115"), Name = "دكرنس", GovernorateId = dakahliaId },
                new City { Id = new Guid("61111111-1111-1111-1111-611111111116"), Name = "المنزلة", GovernorateId = dakahliaId },
                new City { Id = new Guid("61111111-1111-1111-1111-611111111117"), Name = "الدراكسة", GovernorateId = dakahliaId },
                new City { Id = new Guid("61111111-1111-1111-1111-611111111118"), Name = "نبروه", GovernorateId = dakahliaId },

                // الشرقية
                new City { Id = new Guid("71111111-1111-1111-1111-711111111112"), Name = "الزقازيق", GovernorateId = sharqiaId },
                new City { Id = new Guid("71111111-1111-1111-1111-711111111113"), Name = "العاشر من رمضان", GovernorateId = sharqiaId },
                new City { Id = new Guid("71111111-1111-1111-1111-711111111114"), Name = "بلبيس", GovernorateId = sharqiaId },
                new City { Id = new Guid("71111111-1111-1111-1111-711111111115"), Name = "منيا القمح", GovernorateId = sharqiaId },
                new City { Id = new Guid("71111111-1111-1111-1111-711111111116"), Name = "أبو كبير", GovernorateId = sharqiaId },

                // دمياط
                new City { Id = new Guid("81111111-1111-1111-1111-811111111112"), Name = "دمياط", GovernorateId = damiettaId },
                new City { Id = new Guid("81111111-1111-1111-1111-811111111113"), Name = "رأس البر", GovernorateId = damiettaId },
                new City { Id = new Guid("81111111-1111-1111-1111-811111111114"), Name = "كفر سعد", GovernorateId = damiettaId },
                new City { Id = new Guid("81111111-1111-1111-1111-811111111115"), Name = "فارسكور", GovernorateId = damiettaId },

                // القليوبية
                new City { Id = new Guid("91111111-1111-1111-1111-911111111112"), Name = "بنها", GovernorateId = qalyubiaId },
                new City { Id = new Guid("91111111-1111-1111-1111-911111111113"), Name = "شبرا الخيمة", GovernorateId = qalyubiaId },
                new City { Id = new Guid("91111111-1111-1111-1111-911111111114"), Name = "قليوب", GovernorateId = qalyubiaId },

                // القاهرة
                new City { Id = new Guid("10111111-1111-1111-1111-101111111112"), Name = "القاهرة", GovernorateId = cairoId },
                new City { Id = new Guid("10111111-1111-1111-1111-101111111113"), Name = "مدينة نصر", GovernorateId = cairoId },
                new City { Id = new Guid("10111111-1111-1111-1111-101111111114"), Name = "مصر الجديدة", GovernorateId = cairoId },
                new City { Id = new Guid("10111111-1111-1111-1111-101111111115"), Name = "حلوان", GovernorateId = cairoId },
                new City { Id = new Guid("10111111-1111-1111-1111-101111111116"), Name = "الجيزة", GovernorateId = cairoId }
            );
        }
        private static void seedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "a330b209-871f-45fc-9a8d-f357f9bff3b1", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin".ToUpper() },
                new IdentityRole() { Id = "b332b209-871f-45fc-9a8d-f357f9bff3b1", Name = "User", ConcurrencyStamp = "2", NormalizedName = "User".ToUpper() }
                );
        }
        private static void addUser(ModelBuilder modelBuilder)
        {
            var adminUser = new User()
            {
                Id = "7e53a491-a9de-4c75-af44-ff3271a5176c",
                FirstName = "Super",
                LastName = "Admin",
                UserName = "super_admin",
                Email = "super@admin.com",
                EmailConfirmed = true,
                NormalizedUserName = "super_admin".ToUpper(),
                NormalizedEmail = "super@admin.com".ToUpper(),
                Gender = Domain.Enums.Gender.Male,
                MaritalStatus = Domain.Enums.MaritalStatus.Single,
                HasChildren = false,
                PhoneNumber = "01234567891",
                PhoneNumberConfirmed = true,

            };
            var passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "P@ssw0rd");
            modelBuilder.Entity<User>().HasData(adminUser);
        }

        private void assignRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "7e53a491-a9de-4c75-af44-ff3271a5176c", // Admin user ID
                    RoleId = "a330b209-871f-45fc-9a8d-f357f9bff3b1"  // Admin role ID
                }
            );
        }
    }
}
