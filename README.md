
# ✅ Todo API (.NET Core + EF Core + MySQL)

A simple RESTful API built using **ASP.NET Core**, **Entity Framework Core**, and **MySQL** to manage a Todo list.  
The application allows you to **Create**, **Read**, **Update**, and **Delete** (CRUD) todo items.

---

## 📋 Features

- ASP.NET Core Web API
- Entity Framework Core with MySQL
- CRUD operations on todo items
- Unit tests using xUnit + FluentAssertions
- In-memory EF Core for isolated tests
- >90% code coverage

---

## 🔧 Requirements

Before you begin, make sure you have the following installed:

- [.NET SDK 7.0+](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/)
- `dotnet-ef` CLI Tool:

```bash
dotnet tool install --global dotnet-ef
```

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/todo-api.git
cd todo-api
```

### 2. Update MySQL Connection String

Open `TodoApi/appsettings.json` and configure your MySQL credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=TodoDb;user=root;password=yourpassword;"
  }
}
```

> Replace `yourpassword` with your MySQL root password (or custom user/password).

---

### 3. Create the Database Schema

Navigate to the API project folder and run the following:

```bash
cd TodoApi
dotnet ef migrations add InitialCreate
dotnet ef database update
```

These commands will:
- Create migration files under `Migrations/`
- Generate the `TodoDb` schema in your MySQL database

---

### 4. Run the Application

```bash
dotnet run
```

API will be available at:

- `https://localhost:5001`
- `http://localhost:5000`

---

## 📬 API Usage

You can test the API using tools like **Postman**, **curl**, or **Swagger** (if enabled).

### Available Endpoints

| Method | Endpoint             | Description              |
|--------|----------------------|--------------------------|
| GET    | `/api/todoitems`     | Get all todo items       |
| GET    | `/api/todoitems/{id}`| Get a specific item      |
| POST   | `/api/todoitems`     | Create a new item        |
| PUT    | `/api/todoitems/{id}`| Update an existing item  |
| DELETE | `/api/todoitems/{id}`| Delete an item           |

### Example: Create Todo Item

```http
POST /api/todoitems
Content-Type: application/json

{
  "title": "Finish README",
  "description": "Write full instructions for setup"
}
```

---

## 🧪 Run Tests with Coverage

### 1. Navigate to test project

```bash
cd TodoApi.Tests
```

### 2. Run Unit Tests

```bash
dotnet test --collect:"XPlat Code Coverage"
```

> Tests use EF Core InMemory for clean, isolated runs.

### 3. Generate Coverage Report (Optional)

```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coveragereport
```

Then open the report:

```bash
start coveragereport/index.html    # Windows
open coveragereport/index.html     # macOS
xdg-open coveragereport/index.html # Linux
```

---

## 🧱 Project Structure

```
todo-api/
├── TodoApi/                    # Main Web API project
│   ├── Controllers/            # API controllers
│   ├── Models/                 # Data models
│   ├── Data/                   # EF Core DbContext
│   └── appsettings.json        # Configuration
│
├── TodoApi.Tests/              # Test project
│   └── TodoItemsControllerTests.cs
│
├── TodoApi.sln                 # Solution file
└── README.md
```

---

## 🔒 Security Tips

- Never hardcode production credentials in `appsettings.json`
- Use environment variables or [user-secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) for sensitive data

---

## 🛠 Possible Enhancements

- Swagger UI for live documentation
- JWT-based Authentication
- Docker support for easy deployment
- FluentValidation for input validation
- CI/CD with GitHub Actions or Azure DevOps

---

## 📜 License

This project is licensed under the [MIT License](LICENSE).

---

## 🙋‍♂️ Questions?

Feel free to open an issue or contact the maintainer on GitHub.

# Feedback
- Was it easy to complete the task using AI?
Yes

- How long did task take you to complete?
2 hours

- Was the code ready to run after generation? What did you have to change to make it usable?
Not immediately. There were problems with migrations and unit tests. I needed to ask about the solution to these problems.

- Which challenges did you face during completion of the task?
Sometimes, the AI does not understand the question the first time.

- Which specific prompts you learned as a good practice to complete the task?
Using quotation marks to emphasize important points.
