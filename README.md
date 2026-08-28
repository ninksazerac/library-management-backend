# Library Management System Backend

# Setup and Installation

## Prerequisites

Before running the backend, make sure the following are installed:

- .NET 8 SDK
- Microsoft SQL Server
- Git
- Visual Studio or Visual Studio Code


## 1. Clone the Repository

```bash
git clone <repository-url>
cd <backend-folder>
```

## 2. Configure the Database
Open the appsettings.json file and configure the SQL Server connection string.

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<<Server>>;Database=<<Database_Name>>;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## 3. Configure CORS
The backend is configured to allow requests from the local React development server.

The development CORS policy allows the following frontend origin:

```text
http://localhost:5173
```
The configuration is defined in Program.cs:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
```

## 4. Configure User Secrets
The backend uses ASP.NET Core User Secrets to store sensitive development configuration such as JWT settings and database credentials.

Initialize User Secrets for the project:
```bash
dotnet user-secrets init
```
Set the required secrets:
```bash
dotnet user-secrets set "Jwt:Key" "<your-jwt-secret>"
```
If a database connection string is stored as a secret:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your-connection-string>"
```


## 5. Restore Dependencies and Apply Database Migrations

```bash
dotnet restore
```

Create or update the database using Entity Framework Core migrations
```bash
dotnet ef database update
```
If dotnet ef is not installed, install it with
```bash
dotnet tool install --global dotnet-ef
dotnet ef database update
```

## 6. Trust the HTTPS Development Certificate

The backend uses HTTPS in development.

Check whether the ASP.NET Core HTTPS development certificate is available:

```bash
dotnet dev-certs https --check
```
If the certificate is not trusted, run:
```bash
dotnet dev-certs https --trust
```
This allows the local ASP.NET Core API to run using HTTPS without browser certificate warnings.

## 7. Run the Backend
Start the ASP.NET Core Web API
```bash
dotnet run
```

The API will be available at the URL configured in ```launchSettings.json```.
For example: ```http://localhost:7071```

### Development Mode
To run the backend using the HTTPS launch profile
```bash
dotnet run --launch-profile https
```
The API will be available at the HTTPS URL configured in ```Properties/launchSettings.json```.
For example: ```https://localhost:7071```


## 8. Verify the API
Swagger is enabled, open: ```https://localhost:7071/swagger``` (example URL)
