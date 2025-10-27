## API-Practice-CURD

A beginner-friendly C# Windows Forms project that demonstrates how to send and retrieve form data using RESTful API endpoints. This project uses the public CRUD API service from crudcrud.com
, which provides a temporary (24-hour) backend storage with a unique API key.
---
## 📌 Project Objective

This project is designed to help beginners understand how to:
✅ Collect data from a Windows Form
✅ Perform CRUD operations (Create, Read, Update, Delete)
✅ Interact with a REST API using HttpClient
✅ Save and retrieve data from a cloud-based API (crudcrud.com)
---
📂 Project Structure (Example)
```
API-Practice-CURD/
├── APIBase.sln
├── Forms/
│   └── MainForm.cs – Form UI logic
├── Services/
│   └── ApiService.cs – Handles API requests using HttpClient
├── Models/
│   └── Student.cs – Sample data model
└── Program.cs – App entry point
```
📬 Available CRUD Operations (Conceptually)
```
| Feature    | HTTP Method | Endpoint         | Status             |
| ---------- | ----------- | ---------------- | ------------------ |
| Create     | POST        | `/students`      | ✅                  |
| Read All   | GET         | `/students`      | ✅                  |
| Read by ID | GET         | `/students/{id}` | ✅                  |
| Update     | PUT         | `/students/{id}` | ✅ (if implemented) |
| Delete     | DELETE      | `/students/{id}` | ✅ (if implemented) |
```
## 🧪 Testing the API

You can also test your endpoint using tools like:
✅ Postman
✅ Thunder Client (VS Code extension)
✅ curl (optional)

## ⏳ Note on Data Expiry

⚠️ The CRUDCRUD API data expires after 24 hours.
If your API key stops working, simply get a new one and update it in your code.


