# Factory_Pattern-SampleWebAPI

# SampleWebAPI

A .NET 9 Web API project for managing products using multiple repository patterns (Entity Framework, ADO.NET, File-based, and External API). This project demonstrates clean architecture, dependency injection, and flexible repository selection using the Factory Pattern.

## Features

- CRUD operations for `Product` entities.
- Multiple repository implementations:
  - **Entity Framework** for database interaction.
  - **ADO.NET** for raw SQL operations.
  - **File-based** storage using JSON files.
  - **External API** integration.
- Configurable repository selection via query parameters.
- Factory Pattern for dynamic repository creation.
- ASP.NET Core Web API with dependency injection.

## Prerequisites

- .NET 9 SDK
- SQL Server (for Entity Framework and ADO.NET repositories)

## Installation

1. Clone the repository:
   
2. Update the connection string in `appsettings.json`:
   
4. Build and run the project:
   
## Usage

### Endpoints

- **GET** `/api/products/GetAll?repositoryType={type}`: Retrieve all products.
- **GET** `/api/products/GetById?id={id}&repositoryType={type}`: Retrieve a product by ID.
- **POST** `/api/products/Add?repositoryType={type}`: Add a new product.
- **PUT** `/api/products/Update?id={id}&repositoryType={type}`: Update an existing product.
- **DELETE** `/api/products/Delete?id={id}&repositoryType={type}`: Delete a product.

Replace `{type}` with one of the following:
- `EntityFramework`
- `AdoNet`
- `File`
- `External`

### Example Request

Using `SampleWebAPI.http`:

## Factory Pattern

The Factory Pattern is used to dynamically create instances of different repository implementations based on the input type. This allows for flexible and decoupled code.

### Implementation

The `ProductRepositoryFactory` class is responsible for creating repository instances:

### Usage in Controller

The `ProductsController` uses the factory to select the appropriate repository:

### Benefits of the Factory Pattern

- **Flexibility**: Easily switch between different repository implementations.
- **Decoupling**: The controller does not need to know the details of repository creation.
- **Scalability**: Add new repository types without modifying existing code.

## Dependencies

Ensure the following dependencies are installed in your project:

- **Microsoft.EntityFrameworkCore**: For Entity Framework Core.
- **Microsoft.EntityFrameworkCore.SqlServer**: For SQL Server support.
- **System.Text.Json**: For JSON serialization and deserialization.
- **Microsoft.AspNetCore.Mvc**: For building Web APIs.
- **System.Data.SqlClient**: For ADO.NET operations.

Install them using the following command:

## Project Structure

- **Models**: Contains the `Product` entity.
- **Repositories**: Implements repository patterns for different data sources.
- **Controllers**: API endpoints for managing products.
- **Data**: Entity Framework `DbContext` for database interaction.
- **Helper**: Factory for creating repository instances.

## Technologies Used

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- ADO.NET
- JSON Serialization
- SQL Server

## License

This project is licensed under the MIT License.
