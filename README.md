# Public API Data Weaver - Recruitment Task

## Project Description
This project was prepared as a recruitment task for Symphonia. It is a Middleware/Sync Service whose primary goal is to integrate with an external fruit API, process the retrieved information, and permanently archive it in a local database. The system was designed with scalability and code readability in mind, applying modern design patterns within the .NET ecosystem.

## Functionality
* Data Synchronization: Fetching raw data from the external Fruityvice service.
* Data Processing: Mapping external models into internal business entities.
* Persistence: Saving and updating information about fruits and their nutritional values in an MS SQL Server database.
* API Interface: Providing a set of endpoints that allow for:
    * Forcing manual data synchronization.
    * Retrieving a list of fruits from the database.
    * Filtering and searching for information about specific fruits.

## Technologies and Libraries
* Runtime: .NET 8
* Framework: ASP.NET Core Web API
* Database: MS SQL Server
* ORM: Entity Framework Core
* Mapping: AutoMapper
* Documentation: Swagger / OpenAPI
* Testing: xUnit & Fluent Assertion
* Containerization: Docker & Docker Compose

## Setup Instructions
The application is fully containerized, eliminating the need for manual database or .NET environment configuration on your local machine.

1.  Ensure you have Docker Desktop installed.
2.  Open a terminal in the main project folder (where the docker-compose.yml file is located).
3.  Run the command:
  ```
   docker-compose up
  ```
5.  Once the containers are correctly started, the Swagger interface will be available at:
    `http://localhost:8080/swagger/index.html`

## API Overview & Usage
The API is organized into several key functional areas. To ensure a smooth review process, the endpoints are designed to handle empty states gracefully.

1.  Data Ingestion.

      **POST** `/api/fruits/sync`: This is the entry point for the application's data. It fetches raw data from the external API, runs it through the Transformation Engine (applying the Chain of Responsibility logic), and persists the results.

      Note: I designed this as a manual trigger so you can observe the database population in real-time. It is idempotent—syncing multiple times will not create duplicate data or cause errors.

3. Data Retrieval.

   **GET** `/api/fruits`: Returns a list of all processed fruits, including their nutritional information.

   **GET** `/api/fruits/{id}`: Fetches detailed information for a specific fruit by its unique identifier.

5. Advanced Filtering.
   These endpoints demonstrate the ability to query the database based on the results of the transformation logic:

   **GET** `/api/fruits/fitness/{category}`: Filters fruits by their fitness tags (e.g., Keto-Friendly, Protein Boost, Sugar Boom). It uses optimized string-pattern matching to find tags within the serialized category list.

   **GET** `/api/fruits/vitamins/{vitamin}`: Allows searching for fruits that are particularly high in a specific vitamin (e.g., A, C, K) by querying the computed vitamin markers.

## Testing & Quality Assurance
To ensure the reliability of the core business logic, I implemented a suite of unit tests focusing on the Chain of Responsibility pattern used for fruit classification (KetoFriendly, ProteinBoost, and SugarBoom). These tests verify that each link in the chain correctly evaluates nutritional criteria and appropriately modifies the fruit's metadata. While the current test coverage is focused on the most critical transformation logic to keep the project concise, the architecture is fully prepared for expanded test suites using the Arrange-Act-Assert pattern and NSubstitute for dependency isolation.

## Configuration and Security
The docker-compose.yml file contains default authentication credentials (database passwords and logins).

Important security information:
* To facilitate a seamless evaluation process, the Docker environment has been configured to operate in the **Development stage**. This configuration ensures that the **Swagger UI** remains enabled within the containerized environment, allowing for immediate interaction with the API endpoints. Additionally, **HTTPS redirection has been disabled** for the Dockerized build to prevent potential SSL certificate trust issues on local machines, ensuring the service remains fully accessible and functional over standard HTTP.
* Simplified configuration: Passwords have been hardcoded in the configuration file solely to allow for immediate project startup by the recruiter.
* Production standards: In a production environment, sensitive data would be stored using mechanisms such as Docker Secrets, Azure Key Vault, or environment variables injected directly through the CI/CD pipeline.
* Isolation: .env files containing real secrets have been excluded from the version control system in accordance with security best practices.

---
