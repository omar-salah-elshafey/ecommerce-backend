using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            builder.Entity<User>()
                .OwnsMany(u => u.RefreshTokens);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Entity<ProductImage>().HasQueryFilter(pi => !pi.IsDeleted);
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
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Name);
                entity.HasIndex(c => c.ParentCategoryId);
                entity.HasIndex(c => c.IsDeleted);
                entity.HasQueryFilter(c => !c.IsDeleted);
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

            builder.Entity<WishlistItem>(entity =>
            {
                entity.HasIndex(wi => wi.ProductId);
            });

            builder.Entity<Address>(entity =>
            {
                entity.HasIndex(a => a.PostalCode);
            });
            seedRoles(builder);
            addUser(builder);
            assignRole(builder);
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
