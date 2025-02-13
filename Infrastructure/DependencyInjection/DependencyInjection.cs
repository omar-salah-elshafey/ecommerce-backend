using Application.Features.Authentication.Commands.RegisterUser;
using Application.Features.Authentication.Dtos;
using Application.Features.Authentication.Validators;
using Application.Features.Carts.Dtos;
using Application.Features.Carts.Validators;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Validators;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Validators;
using Application.Features.PasswordManagement.Dtos;
using Application.Features.PasswordManagement.Validators;
using Application.Features.Products.Dtos;
using Application.Features.Products.Validators;
using Application.Features.Reviews.Dtos;
using Application.Features.Reviews.Validators;
using Application.Features.UserManagement.Dtos;
using Application.Features.UserManagement.Validators;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnections")));

            services.AddHttpContextAccessor();
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(5);
            });
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly));
            // Register application validators
            services.AddScoped<IValidator<RegistrationDto>, RegistrationDtoValidator>();
            services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddScoped<IValidator<ChangePasswordDto>, ChangePasswordDtoValidator>();
            services.AddScoped<IValidator<ResetPasswordDto>, ResetPasswordDtoValidator>();
            services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
            services.AddScoped<IValidator<ChangeUserRoleDto>, ChangeUserRoleDtoValidator>();
            services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryValidator>();
            services.AddScoped<IValidator<UpdateCategoryDto>, UpdateCategoryValidator>();
            services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();
            services.AddScoped<IValidator<UpdateProductDto>, UpdateProductDtoValidator>();
            services.AddScoped<IValidator<CreateReviewDto>, CreateReviewDtoValidator>();
            services.AddScoped<IValidator<UpdateReviewDto>, UpdateReviewDtoValidator>();
            services.AddScoped<IValidator<CartItemChangeDto>, CartItemChangeDtoValidator>();
            services.AddScoped<IValidator<CreateOrderDto>, CreateOrderDtoValidator>();
            // Register application services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICookieService, CookieService>();
            // Register application repositories 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IGovernorateRepository, GovernorateRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}
