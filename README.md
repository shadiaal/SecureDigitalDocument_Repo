
# Secure Digital Document Verification System

## Project Overview
This project implements a Secure Digital Document Verification System that allows users to upload documents and verify them using unique digital codes. The system leverages ASP.NET Core, Angular, and SQL Server to provide a secure and efficient solution for document verification.

## Technologies
- Backend: ASP.NET Core, Entity Framework Core, Dapper
- Frontend: Angular, TypeScript
- Database: SQL Server

## Setup Instructions
### Backend
Clone the repository:
```bash
git clone https://github.com/your-username/secure-document-verification.git
cd secure-document-verification
```

Install dependencies and run migrations:
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Dapper
dotnet ef migrations add InitialMigration
dotnet ef database update
```

Run the backend server:
```bash
dotnet run
```

### Frontend
Install Node.js and Angular CLI.
Install frontend dependencies:
```bash
npm install
```

Run the Angular development server:
```bash
ng serve
```

## API Documentation

### 1. Upload Document
**Endpoint**: `POST /api/documents/upload`

This endpoint allows the user to upload a document for verification.

#### Request:
- **Method**: POST
- **URL**: `/api/documents/upload`
- **Headers**:
  - `Content-Type: application/json`

**Request Body** (JSON):
```json
{
  "file": "<base64_encoded_file>",      // Base64 encoded file content
  "documentType": "Invoice",           // Type of the document (e.g., Invoice, Passport, etc.)
  "userId": 1                          // ID of the user uploading the document
}
```

#### Response:
- **Status**: 200 OK
- **Response Body** (JSON):
```json
{
  "documentId": 1,
  "message": "Document uploaded successfully."
}
```

### 2. Verify Document
**Endpoint**: `POST /api/documents/verify`

This endpoint allows the user to verify the uploaded document using the unique verification code.

#### Request:
- **Method**: POST
- **URL**: `/api/documents/verify`
- **Headers**:
  - `Content-Type: application/json`

**Request Body** (JSON):
```json
{
  "verificationCode": "ABC123XYZ"   // Unique code generated for document verification
}
```

#### Response:
- **Status**: 200 OK
- **Response Body** (JSON):
```json
{
  "isVerified": true,                 // Status of the document verification
  "message": "Document verification successful."
}
```

If verification fails:
```json
{
  "isVerified": false,
  "message": "Invalid verification code."
}
```

### 3. Get Document Details
**Endpoint**: `GET /api/documents/{documentId}`

This endpoint retrieves the details of a specific document by its `documentId`.

#### Request:
- **Method**: GET
- **URL**: `/api/documents/{documentId}`
  - Replace `{documentId}` with the ID of the document you want to retrieve.

#### Response:
- **Status**: 200 OK
- **Response Body** (JSON):
```json
{
  "documentId": 1,
  "fileName": "invoice.pdf",
  "documentType": "Invoice",
  "userId": 1,
  "uploadDate": "2025-03-08T12:30:00Z",
  "verificationStatus": "Verified"
}
```

### 4. Get All Document Details
**Endpoint**: `GET /api/documents`

This endpoint retrieves a list of all uploaded documents.

#### Request:
- **Method**: GET
- **URL**: `/api/documents`

#### Response:
- **Status**: 200 OK
- **Response Body** (JSON):
```json
[
  {
    "documentId": 1,
    "fileName": "invoice.pdf",
    "documentType": "Invoice",
    "userId": 1,
    "uploadDate": "2025-03-08T12:30:00Z",
    "verificationStatus": "Verified"
  },
  {
    "documentId": 2,
    "fileName": "passport.jpg",
    "documentType": "Passport",
    "userId": 2,
    "uploadDate": "2025-03-08T14:00:00Z",
    "verificationStatus": "Pending"
  }
]
```
