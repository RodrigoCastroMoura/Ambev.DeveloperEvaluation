
# Ambev Developer Evaluation

This project is a full-stack application built with .NET 8 and Angular.

## Prerequisites

- .NET SDK 8.0
- Node.js and npm
- SQL Server

## Project Structure

- `src/Ambev.DeveloperEvaluation.WebApi` - Backend API
- `src/Ambev.DeveloperEvaluation.WebApp` - Frontend Angular application
- `tests/` - Unit, Integration, and Functional tests

## Setup

1. Clone the repository
2. Update database connection string in `src/Ambev.DeveloperEvaluation.WebApi/appsettings.json`
3. Install dependencies:

```bash
# Backend
dotnet restore

# Frontend
cd src/Ambev.DeveloperEvaluation.WebApp
npm install
```

## Running the Application

### Backend API

```bash
cd src/Ambev.DeveloperEvaluation.WebApi
dotnet run
```
The API will be available at `http://0.0.0.0:5000`

### Frontend Application

```bash
cd src/Ambev.DeveloperEvaluation.WebApp
npm start
```
The application will be available at `http://0.0.0.0:4200`

## Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/Ambev.DeveloperEvaluation.Unit/Ambev.DeveloperEvaluation.Unit.csproj

# Generate coverage report
./coverage-report.sh  # For Linux/Mac
coverage-report.bat   # For Windows
```

## API Documentation

The API documentation is available through Swagger at `http://0.0.0.0:5000/swagger`

## Features

- User Authentication
- Product Management
- Sales Management
- Unit Tests
- Integration Tests
- API Documentation

## Project Architecture

- Clean Architecture
- CQRS Pattern
- Repository Pattern
- Domain-Driven Design
