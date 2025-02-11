# IcebergLog Community Repository

IcebergLog is a **high-performance, multi-database log management library** designed for .NET applications. It supports multiple storage backends such as **MongoDB, PostgreSQL, and Elasticsearch**. Additionally, it provides **structured logging** and real-time log processing.

IcebergLog follows an **Open Core Model**, where the core functionalities are open-source, while advanced features such as AI-powered log analysis, enhanced security, and enterprise integrations are available under a commercial license.

## 🚀 Features

- ✅ **Multi-Database Support** (MongoDB, PostgreSQL, Elasticsearch)
- ✅ **Asynchronous & Synchronous Logging**
- ✅ **gRPC & REST API Integration**
- ✅ **Log Filtering & Masking for Security**
- ✅ **Dependency Injection Support**
- ✅ **Multi-Logger Support (Logging to multiple databases simultaneously)**
- ✅ **Serilog Integration for File & Console Logging**

## 🔍 Community vs Enterprise Edition
IcebergLog is offered in two versions: **Community Edition** (Open Source) and **Enterprise Edition** (Commercial).

| Feature                            | Community Edition (Free & Open Source) | Enterprise Edition (Commercial) |
|------------------------------------|----------------------------------|--------------------------------|
| Multi-Database Support            | ✅ MongoDB, PostgreSQL, Elasticsearch | ✅ MongoDB, PostgreSQL, Elasticsearch |
| Asynchronous & Synchronous Logging | ✅ Yes                               | ✅ Yes |
| gRPC & REST API Integration       | ✅ Yes                               | ✅ Yes |
| Log Filtering & Masking            | ✅ Yes                               | ✅ Yes |
| AI-Powered Log Analysis            | ❌ No                                | ✅ Yes (Advanced Log Insights) |
| Log Anomaly Detection              | ❌ No                                | ✅ Yes (Machine Learning Based) |
| Real-time Log Monitoring Dashboard | ❌ No                                | ✅ Yes (Web UI & Alerts) |
| Enterprise Security & Compliance   | ❌ No                                | ✅ Yes (SOC2, GDPR Compliance) |
| High Availability & Scalability    | ❌ No                                | ✅ Yes (Cloud & On-Prem) |
| Custom Integrations & Support      | ❌ No                                | ✅ Yes (24/7 Support & SLA) |

📌 **Community Edition** is free and open-source, perfect for individual developers and small projects. 
📌 **Enterprise Edition** is designed for businesses needing advanced features, AI-driven insights, and premium support.

 

## 📥 Installation & Setup
### **1️⃣ Clone the repository**
```sh
git clone https://github.com/yourusername/IcebergLog-Community.git
cd IcebergLog-Community
```
### **2️⃣ Install dependencies**
Ensure that you have **.NET 8.0 SDK** installed.
```sh
dotnet restore
```
### **3️⃣ Set up Database Configuration**
Modify `appsettings.json` for your preferred storage:
```json
"LogStorage": {
    "Type": "MongoDB",
    "ConnectionString": "mongodb://localhost:27017"
}
```
Supported storage options: `"MongoDB"`, `"PostgreSQL"`, `"Elasticsearch"`

### **4️⃣ Build & Run the Project**
```sh
dotnet build
dotnet run --project src/IcebergLog.API/IcebergLog.API.csproj
```

### **5️⃣ Running Tests**
```sh
dotnet test
```

## 🛠️ Usage
### **Logging via API**
Send a `POST` request to `/log` with the following JSON payload:
```json
{
    "Level": "Info",
    "Message": "This is a test log",
    "Source": "API"
}
```
Supported log levels: `Debug`, `Info`, `Warning`, `Error`, `Critical`

### **gRPC Logging**
Use the gRPC client to send logs:
```csharp
var client = new LogServiceGrpcClient(channel);
var response = await client.WriteLogAsync(new LogRequest
{
    Level = "Error",
    Message = "An exception occurred!",
    Source = "WorkerService"
});
```

## 🧪 Testing
- The project uses **xUnit** for unit testing.
- **Moq** is used for mocking dependencies.
- Run all tests:
```sh
dotnet test
```

## 💡 Roadmap
- 🔹 **AI Log Pattern Detection**
- 🔹 **Real-time Log Monitoring UI**
- 🔹 **Support for More Databases**
- 🔹 **Log Retention Policies**

## 👥 Contributing
1. **Fork the repository**
2. **Create a new branch** (`git checkout -b feature-name`)
3. **Commit your changes** (`git commit -m "Added new feature"`)
4. **Push to the branch** (`git push origin feature-name`)
5. **Create a pull request**

## 📜 License
This project is licensed under the **Apache 2.0 License**.

 
 
🌍 **Website-coming soon:** [iceberglog.com](https://iceberglog.com)\
 
