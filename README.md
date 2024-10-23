# URLShortener

🚀 I'm thrilled to announce the release of **URLShortener**, a comprehensive and feature-rich repository for making URLs short in .NET 8 applications.

---

## 🌟 What’s Inside URLShortener?

This repository is packed with:

### 🚨 Functionality

- ➡ Make a URL in short form of **five chars**
- ➡ Supports about **one Billion URLs**
- ➡ Use 'A' to 'Z', 'a' to 'z', and '0' to '9' to generate short forms
- ➡ Sample: "http://localhost:5249/𝚛𝚗𝚟𝚜"
- ➡ **Count Click** count of each short URL
- ➡ **Redirect** short URL to the original one

### 🔧 Core Architecture & Design Patterns

- ➡ **Clean Architecture**
- ➡ **CQRS** pattern for separating read and write operations
- ➡ **Repository** patterns
- ➡ **EF Core**

### 🗄️ Database

- ➡ **SQL Server**

### 🛠️ Middleware & Error Handling

- ➡ **Custom Middleware** for cross-cutting concerns
- ➡ **Error Handlers** for HTTP status codes (400, 404, 409, 500)
- ➡ **BaseResult** pattern for uniform API responses

### 📊 Health Monitoring

- ➡ **Health Checks** in route `/healthz`

### 🐳 Docker & DevOps

- ➡ **Docker** support for containerization

### 📋 Swagger & API Management

- ➡ Fully configured **Swagger** with security, examples

### 🔰 Tests

- ➡ Unit tests
- ➡ Integration tests
- ➡ Functional tests
- ➡ Load tests

🛑 See detail of tests in the below post  
[Testing Details](https://www.linkedin.com/posts/behzaddara_tests-unittest-integrationtest-activity-7240275533043781632-EBQ0?utm_source=share&utm_medium=member_desktop)

### 📌 Additional Tools & Patterns

- ➡ **Multi-language** response based on header for En and Fa
- ➡ Best practices in **DDD** and **OOP**

🛑 Used **Random.Shared**, a thread-safe feature introduced in .NET 8

🛑 Used **Original** and **Shortened** as Key to remove LoadAll before generating shortened URL and used **try-catch** to insert data

---

## 🔗 Explore the Repository

You can find all these features and more in the **URLShortener** repository on GitHub. Feel free to explore, fork, and contribute!  
👉 [GitHub Repository](https://lnkd.in/d6YXFT83)

🌟 Don't forget to give a **STAR** ⭐ 

---

## 🤝 Get Involved!

Contributions, feedback, and suggestions are highly welcome! Let’s collaborate to make **URLShortener** better.
