# E-Commerce Backend

## Overview
This is the backend for an e-commerce platform, built using **ASP.NET Core 8** following **Clean Architecture** principles. 
It provides API endpoints for product management, authentication, order processing, and more.
The front-end project linked to this project: [E-Commerce front-end](https://github.com/omar-salah-elshafey/ecommerce-frontend)

## Technologies Used
- **ASP.NET Core 8**
- **Entity Framework Core** (EF Core) with SQL Server
- **MediatR** (CQRS pattern)
- **FluentValidation** for input validation
- **AutoMapper** for object mapping
- **JWT Authentication** for secure API access

## Architecture
The project follows the **Clean Architecture** structure:

- **Domain Layer:** Contains core business logic and entity definitions.
- **Application Layer:** Contains services, DTOs, business rules, and MediatR handlers.
- **Infrastructure Layer:** Handles database access (EF Core), repositories, and third-party services.
- **API Layer:** Exposes RESTful endpoints and integrates with the application layer.

---

## Features Implemented

### 🔐 Authentication & Authorization
- JWT-based authentication
- User roles: **Admin, Customer**
- User registration & login
- Password hashing & validation

### 📦 Product Management
- CRUD operations for products
- **Pagination** for product listings
- **Featured Products Endpoint** (paginated)
- **Best Seller Products Endpoint**
- Product images support (stored in `wwwroot` for now)

### 🛒 Cart & Wishlist
- Add & remove items
- Wishlist and cart dynamically fetch the latest product price

### 📂 Category Management
- CRUD operations for product categories
- Repository pattern used for database interaction

### 🏙️ Location Management
- Retrieve **governorates and cities**
- Validate governorate & city IDs when placing an order

### 📦 Order Management
- Place an order (cart items are converted into an order)
- **Order Status Updates:** Order progresses through `Pending → Processing → Shipped → Delivered → Cancelled`
- **Only Cash on Delivery (COD)** is supported for now
- Potential future feature: **Shipping cost calculation based on location**

### ⚙️ Admin Dashboard Support
- Endpoints to manage **users, orders, and products**
- View sales and best-seller products

---

## API Endpoints
### Authentication
- `POST /api/auth/register` – Register a new user
- `POST /api/auth/login` – Authenticate user and get JWT token

### Products
- `GET /api/products?pageNumber={page}&pageSize={size}` – Get paginated product list
- `GET /api/products/{id}` – Get product by ID
- `GET /api/products/get-featured-products` – Get featured products
- `GET /api/products/get-best-seller` – Get top-selling products
- `POST /api/products` – Add a new product (Admin only)
- `PUT /api/products/{id}` – Update a product (Admin only)
- `DELETE /api/products/{id}` – Soft delete a product (Admin only)

### Orders
- `POST /api/orders` – Place an order
- `PUT /api/orders/update-status` – Update order status (Admin only)

### Location
- `GET /api/governorates` – Get all governorates
- `GET /api/governorates/{id}/cities` – Get cities by governorate

---

## Installation & Setup
### 1️⃣ Clone the repository
```bash
git clone https://github.com/your-repo-url.git
cd e-commerce-backend
```
### 2️⃣ Configure Database Connection
Edit `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ECommerceDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
}
```
### 3️⃣ Run Migrations
```bash
dotnet ef database update
```
### 4️⃣ Run the Application
```bash
dotnet run
```
The API will be available at `http://localhost:5000` (or a different port if configured).

---

## Next Steps
✅ **Improve shipping logic & potential cost calculation**
✅ **Optimize database queries**
❌ Add payment gateway integration (e.g., Stripe, PayPal)

