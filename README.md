# 📚 Library Management System - User Manual

Welcome to the **Library Management System (LMS)** developed using **ASP.NET Core 8 (MVC)** with **Entity Framework Core** and **SQL Server**. This document provides step-by-step instructions for cloning, configuring, and running the project locally.

---

## 🚀 Prerequisites

Make sure the following tools are installed on your system:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or any editor of your choice
- [Git](https://git-scm.com/)
- (Optional) [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-ssms)

---

## 🧾 Steps to Run the Project

### 1. Clone the Repository

```bash
git clone https://github.com/mdrezaulkarim38/LibraryManagementSystem.git
cd LibraryManagementSystem
````

---

### 2. Configure the Database Connection

* Open the project in your editor.
* Go to the `appsettings.json` file.
* Locate the `"DefaultConnection"` under `ConnectionStrings`.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> ⚠️ Replace `YOUR_SERVER_NAME` with your SQL Server instance name.
> You can also use `localhost`, `.` (dot), or `(localdb)\\MSSQLLocalDB` depending on your setup.

---

### 3. Create the Database

Run the following commands in your terminal:

```bash
dotnet restore
dotnet ef database update
```

This will apply all the Entity Framework Core migrations and create the database with the necessary tables.

---

### 4. Run the Application

```bash
dotnet run
```

The app will start, and you will see a message like:

```
Now listening on: http://localhost:XXXX
```

Open that URL in your browser to start using the system.

---

## 🔑 Default Features

* ✅ User Registration/Login (Admin & User)
* ✅ Add/View Book Categories
* ✅ Add/View Books
* ✅ Borrow Books (if available)
* ✅ Activate/Deactivate Users (Admin only)

---

## 🔧 Troubleshooting

* Make sure your SQL Server is **running** and **accepting connections**.
* If you get EF or SQL errors, ensure the connection string is correct and you have **sufficient permissions**.
* If using `dotnet ef` commands gives an error, install the EF CLI:

```bash
dotnet tool install --global dotnet-ef
```

---

## 📂 Project Structure

```
LibraryManagementSystem/
├── Controllers/
├── Models/
├── Views/
├── Data/
├── Migrations/
├── wwwroot/
├── appsettings.json
└── Program.cs
```

## 👤 Author

**Rezaul Karim**
GitHub: [mdrezaulkarim38](https://github.com/mdrezaulkarim38)
