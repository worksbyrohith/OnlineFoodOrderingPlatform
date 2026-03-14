<p align="center">
  <img src="https://img.shields.io/badge/🔥-BurntBrotta-FF6B35?style=for-the-badge&labelColor=1a1a2e" alt="BurntBrotta" />
</p>
<p align="center"> 
  <img src="https://user-images.githubusercontent.com/74038190/212284100-561aa473-3905-4a80-b561-0d28506553ee.gif" width="700">
<center>

 <h1 align="center">🍕 BurntBrotta — Online Food Ordering Platform</h1>

</center>
<p align="center"> 
  <img src="https://user-images.githubusercontent.com/74038190/212284100-561aa473-3905-4a80-b561-0d28506553ee.gif" width="700">

<p align="center">
  <em>A full-stack food ordering application built with Angular &amp; ASP.NET Core Web API</em>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Angular-19-DD0031?style=flat-square&logo=angular&logoColor=white" />
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-Express-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/JWT-Auth-000000?style=flat-square&logo=jsonwebtokens&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework-Core-purple?style=flat-square" />
  <img src="https://img.shields.io/badge/License-MIT-green?style=flat-square" />
</p>

---

## 📖 About

**BurntBrotta** is a complete online food ordering platform where customers can browse restaurants, view menus, add items to cart, place orders, and simulate payments. Admins get a full dashboard to manage restaurants, menu items, and track orders in real-time.

### ✨ Key Features

| 👤 Customer Features | 🔧 Admin Features |
|---|---|
| 🔐 Register & Login (JWT) | 📊 Dashboard with live stats |
| 🏪 Browse active restaurants | 🏪 Add / Edit / Delete restaurants |
| 🍔 View menu items by restaurant | 🍔 Add / Edit / Toggle menu items |
| 🛒 Add to cart & place orders | 📦 View & manage all orders |
| 💳 Simulate payment processing | 👥 View all registered users |
| 📋 View order history | 🔄 Update order status |

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────┐
│                    CLIENT (Angular)                 │
│    Components → Services → HTTP Interceptors        │
└──────────────────────┬──────────────────────────────┘
                       │ HTTP/REST
                       ▼
┌─────────────────────────────────────────────────────┐
│               ASP.NET Core Web API                  │
│                                                     │
│  ┌─────────────┐  ┌──────────────┐  ┌────────────┐  │
│  │ Controllers │→ │   Services   │→ │ Repository │  │
│  │ (API Layer) │  │(Business     │  │ (Data      │  │
│  │             │  │  Logic)      │  │  Access)   │  │
│  └─────────────┘  └──────────────┘  └─────┬──────┘  │
│                                            │        │
│  ┌─────────────┐  ┌──────────────┐         │        │
│  │    DTOs     │  │  JWT Helper  │         │        │
│  └─────────────┘  └──────────────┘         │        │
└────────────────────────────────────────────┼────────┘
                                             │ EF Core
                                             ▼
                                    ┌──────────────┐
                                    │  SQL Server  │
                                    │  (Database)  │
                                    └──────────────┘
```

---

## 🛠️ Tech Stack

### Frontend
| Technology | Purpose |
|---|---|
| **Angular 19** | SPA framework |
| **TypeScript** | Type-safe JavaScript |
| **Bootstrap / Custom CSS** | Responsive UI styling |
| **Poppins (Google Fonts)** | Typography |
| **RxJS** | Reactive HTTP calls |

### Backend
| Technology | Purpose |
|---|---|
| **ASP.NET Core 10** | Web API framework |
| **Entity Framework Core** | ORM (Object-Relational Mapping) |
| **SQL Server Express** | Relational database |
| **JWT Bearer** | Authentication & authorization |
| **BCrypt.NET** | Secure password hashing |

---

## 📁 Project Structure

```
OnlineFoodOrderingPlatform/
│
├── 📂 FoodOrderingPlatform.Api/          # Backend (.NET Web API)
│   ├── Controllers/                      # API endpoints
│   │   ├── AuthController.cs             # Login & Register
│   │   ├── RestaurantsController.cs      # Restaurant CRUD
│   │   ├── MenuItemsController.cs        # Menu item CRUD
│   │   ├── OrdersController.cs           # Order management
│   │   ├── PaymentsController.cs         # Payment processing
│   │   └── AdminController.cs            # Admin dashboard
│   ├── Models/                           # Database entities
│   │   ├── User.cs, Restaurant.cs, MenuItem.cs
│   │   ├── Order.cs, OrderItem.cs, Payment.cs
│   ├── Services/                         # Business logic layer
│   ├── Repositories/                     # Generic repository pattern
│   ├── DTOs/                             # Data transfer objects
│   ├── Data/AppDbContext.cs              # EF Core DB context
│   ├── Helpers/JwtHelper.cs              # JWT token generator
│   ├── Program.cs                        # App entry point
│   └── appsettings.json                  # Configuration
│
├── 📂 FoodOrderingPlatform.Client/       # Frontend (Angular)
│   └── src/app/
│       ├── components/
│       │   ├── admin/                    # Admin dashboard, orders, restaurants, menu
│       │   ├── auth/                     # Login & Register pages
│       │   ├── cart/                     # Shopping cart
│       │   ├── menu/                     # Menu display
│       │   ├── order/                    # Order history
│       │   ├── restaurant/              # Restaurant listing
│       │   └── shared/                  # Navbar, Footer, Loader
│       ├── services/                    # API communication services
│       ├── guards/                      # Auth & Admin route guards
│       ├── interceptors/                # JWT interceptor
│       └── models/                      # TypeScript interfaces
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/) & npm
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Angular CLI](https://angular.dev/) (`npm install -g @angular/cli`)

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/your-username/OnlineFoodOrderingPlatform.git
cd OnlineFoodOrderingPlatform
```

### 2️⃣ Setup the Backend

```bash
cd FoodOrderingPlatform.Api

# Restore NuGet packages
dotnet restore

# Update database connection string in appsettings.json if needed
# Default: Server=.\SQLEXPRESS;Database=FoodOrderingDB;...

# Apply database migrations
dotnet ef database update

# Run the backend
dotnet run
```

> 🟢 Backend will start at `http://localhost:5231` (or the port in launchSettings.json)

### 3️⃣ Setup the Frontend

```bash
cd FoodOrderingPlatform.Client

# Install dependencies
npm install

# Start the development server
ng serve
```

> 🟢 Frontend will start at `http://localhost:4200`

### 4️⃣ Default Admin Login

| Field | Value |
|---|---|
| Email | `admin@food.com` |
| Password | `Admin@123` |

> The admin user is automatically seeded when the backend starts for the first time.

---

## 🔌 API Endpoints

### 🔐 Authentication
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `POST` | `/api/Auth/register` | Register new user | ❌ |
| `POST` | `/api/Auth/login` | Login & get JWT token | ❌ |

### 🏪 Restaurants
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `GET` | `/api/Restaurants` | Get all active restaurants | ❌ |
| `GET` | `/api/Restaurants/{id}` | Get restaurant by ID | ❌ |
| `POST` | `/api/Restaurants` | Create restaurant | 🔒 Admin |
| `PUT` | `/api/Restaurants/{id}` | Update restaurant | 🔒 Admin |
| `DELETE` | `/api/Restaurants/{id}` | Soft-delete restaurant | 🔒 Admin |

### 🍔 Menu Items
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `GET` | `/api/MenuItems/restaurant/{id}` | Get menu by restaurant | ❌ |
| `GET` | `/api/MenuItems/{id}` | Get item by ID | ❌ |
| `POST` | `/api/MenuItems` | Create menu item | 🔒 Admin |
| `PUT` | `/api/MenuItems/{id}` | Update menu item | 🔒 Admin |
| `DELETE` | `/api/MenuItems/{id}` | Delete menu item | 🔒 Admin |
| `PATCH` | `/api/MenuItems/{id}/availability` | Toggle availability | 🔒 Admin |

### 📦 Orders
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `POST` | `/api/Orders` | Place an order | 🔒 Customer |
| `GET` | `/api/Orders/my` | Get my orders | 🔒 Customer |
| `GET` | `/api/Orders/{id}` | Get order details | 🔒 Owner/Admin |
| `GET` | `/api/Orders` | Get all orders (filterable) | 🔒 Admin |
| `PATCH` | `/api/Orders/{id}/status` | Update order status | 🔒 Admin |

### 💳 Payments
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `POST` | `/api/Payments/process/{orderId}` | Process payment | 🔒 Customer |
| `GET` | `/api/Payments/{orderId}` | Get payment details | 🔒 User |

### 📊 Admin
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `GET` | `/api/Admin/dashboard` | Get dashboard stats | 🔒 Admin |
| `GET` | `/api/Admin/users` | Get all users | 🔒 Admin |

---

## 🗄️ Database Schema

```
┌──────────┐     ┌──────────────┐    ┌───────────┐
│  Users   │     │  Restaurants │    │ MenuItems │
├──────────┤     ├──────────────┤    ├───────────┤
│ UserId   │     │ RestaurantId │◄───┐ ItemId    │
│ Name     │     │ Name         │    │Restaurant │
│ Email *  │     │ Address      │    │  Id (FK)  │
│ Password │     │ Phone        │    │ Name      │
│ Hash     │     │ ImageUrl     │    │ Desc      │
│ Role     │     │ IsActive     │    │ Price     │
│ CreatedAt│     └──────────────┘    │ Category  │
└─────┬────┘                         │ IsAvail.  │
      │                              └─────┬─────┘
      │         ┌───────────┐              │
      │         │  Orders   │              │
      │         ├───────────┤              │
      └────────►│ OrderId   │              │
                │ UserId(FK)│    ┌─────────┴──┐
                │ Restaur.Id│    │ OrderItems │
                │ Total Amt │    ├────────────┤
                │ Status    │◄───│ OrderItemId│
                │ Address   │    │ OrderId(FK)│
                │ OrderedAt │    │ ItemId(FK) │
                └─────┬─────┘    │ Quantity   │
                      │          │ UnitPrice  │
                ┌─────┴─────┐    └────────────┘
                │ Payments  │
                ├───────────┤
                │ PaymentId │
                │ OrderId * │
                │ Amount    │
                │ Method    │
                │ Status    │
                │ PaidAt    │
                └───────────┘
```

---

## 🔒 Security Features

- 🔑 **JWT Authentication** with 2-hour token expiry
- 🔐 **BCrypt Password Hashing** — plaintext passwords are never stored
- 👥 **Role-Based Authorization** — Admin vs Customer access control
- 🛡️ **CORS Policy** — restricts API access to trusted origins
- ✅ **Input Validation** — Data Annotations on all DTOs and Models
- 🔄 **Database Transactions** — atomic operations for orders & payments

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

---

<p align="center">
  Built with ❤️ by <strong>Rohith</strong> 
</p>
