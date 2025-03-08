# Secure Digital Document Verification System

## Project Overview
This project implements a Secure Digital Document Verification System that allows users to upload documents and verify them using unique digital codes. The system leverages ASP.NET Core, Angular, and SQL Server to provide a secure and efficient solution for document verification.


## Technologies
- Backend: ASP.NET Core, Entity Framework Core, Dapper
- Frontend: Angular, TypeScript
- Database: SQL Server
  
## Setup Instructions
- Backend
Clone the repository:
- ```git clone https://github.com/your-username/secure-document-verification.git```
- ```cd secure-document-verification```
Install dependencies and run migrations:

- ```dotnet add package Microsoft.EntityFrameworkCore```
- ```dotnet add package Microsoft.EntityFrameworkCore.Tools```
- ```dotnet add package Microsoft.EntityFrameworkCore.Design```
- ```dotnet add package Dapper```
- ```dotnet ef migrations add InitialMigration```

- ```dotnet ef database update```
Run the backend server:
```dotnet run```

- Frontend
Install Node.js and Angular CLI.
Install frontend dependencies:
- ```npm install```
Run the Angular development server:
- ```ng serve```
The frontend will be running at http://localhost:4200.

API Documentation
1. Upload Document
- Endpoint: POST /api/documents/upload
2. Verify Document
- Endpoint: POST /api/documents/verify
3. Get Document Details
- Endpoint: GET /api/documents/{documentId}
4. Get All Document Details
- Endpoint: GET /api/documents

