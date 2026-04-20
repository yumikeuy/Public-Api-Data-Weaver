# Public API Data Weaver - Zadanie Rekrutacyjne

## Opis projektu
Projekt został przygotowany jako zadanie rekrutacyjne dla firmy Symphonia. Jest to usługa typu Middleware/Sync Service, której głównym celem jest integracja z zewnętrznym API owoców, przetwarzanie uzyskanych informacji oraz ich trwała archiwizacja w lokalnej bazie danych. System został zaprojektowany z myślą o skalowalności i czytelności kodu, stosując nowoczesne wzorce projektowe w ekosystemie .NET.

## Funkcjonalność
* Synchronizacja danych: Pobieranie surowych danych z zewnętrznego serwisu Fruityvice.
* Przetwarzanie danych: Mapowanie zewnętrznych modeli na wewnętrzne encje biznesowe.
* Persystencja: Zapisywanie i aktualizowanie informacji o owocach oraz ich wartościach odżywczych w bazie MS SQL Server.
* Interfejs API: Udostępnienie zestawu endpointów umożliwiających:
    * Wymuszenie ręcznej synchronizacji danych.
    * Pobieranie listy owoców z bazy danych.
    * Filtrowanie i wyszukiwanie informacji o konkretnych owocach.

## Technologie i biblioteki
* Runtime: .NET 8
* Framework: ASP.NET Core Web API
* ORM: Entity Framework Core (MS SQL Server)
* Mapowanie: AutoMapper
* Dokumentacja: Swagger / OpenAPI
* Konteneryzacja: Docker & Docker Compose

## Instrukcja uruchomienia
Aplikacja została w pełni skonteneryzowana, co eliminuje potrzebę ręcznej konfiguracji bazy danych czy środowiska .NET na maszynie lokalnej.

1.  Upewnij się, że masz zainstalowany program Docker Desktop.
2.  Otwórz terminal w głównym folderze projektu (tam, gdzie znajduje się plik docker-compose.yml).
3.  Uruchom komendę:
  ```
   docker-compose up
  ```
5.  Po poprawnym uruchomieniu kontenerów, interfejs Swagger będzie dostępny pod adresem:
    `http://localhost:8080/swagger/index.html`

## API Overview & Usage
The API is organized into several key functional areas. To ensure a smooth review process, the endpoints are designed to handle empty states gracefully.

1.  Data Ingestion.

      POST `/api/fruits/sync`: This is the entry point for the application's data. It fetches raw data from the external API, runs it through the Transformation          Engine (applying the Chain of Responsibility logic), and persists the results.

      Note: I designed this as a manual trigger so you can observe the database population in real-time. It is idempotent—syncing, multiple times will not create         duplicate data or cause errors.

3. Data Retrieval.

   GET `/api/fruits`: Returns a list of all processed fruits, including their nutritional information.

   GET `/api/fruits/{id}`: Fetches detailed information for a specific fruit by its unique identifier.

5. Advanced Filtering.
   These endpoints demonstrate the ability to query the database based on the results of the transformation logic:

   GET `/api/fruits/fitness/{category}`: Filters fruits by their fitness tags (e.g., KetoFriendly, ProteinBoost, SugarBoom). It uses optimized string-pattern          matching to find tags within the serialized category list.

   GET `/api/fruits/vitamins/{vitamin}`: Allows searching for fruits that are particularly high in a specific vitamin (e.g., A, C, K) by querying the computed         vitamin markers.

## Testing & Quality Assurance
To ensure the reliability of the core business logic, I implemented a suite of unit tests focusing on the Chain of Responsibility pattern used for fruit classification (KetoFriendly, ProteinBoost, and SugarBoom). These tests verify that each link in the chain correctly evaluates nutritional criteria and appropriately modifies the fruit's metadata. While the current test coverage is focused on the most critical transformation logic to keep the project concise, the architecture is fully prepared for expanded test suites using the AAA (Arrange-Act-Assert) pattern and NSubstitute for dependency isolation.

## Konfiguracja i bezpieczeństwo
W pliku docker-compose.yml zostały zawarte domyślne dane uwierzytelniające (hasła i loginy do bazy danych). 

Ważne informacje dotyczące bezpieczeństwa:
* Uproszczona konfiguracja: Hasła zostały wpisane na sztywno w pliku konfiguracyjnym wyłącznie w celu umożliwienia natychmiastowego uruchomienia projektu przez osobę rekrutującą.
* Standardy produkcyjne: W środowisku produkcyjnym dane wrażliwe byłyby przechowywane z wykorzystaniem mechanizmów takich jak Docker Secrets, Azure Key Vault lub zmienne środowiskowe wstrzykiwane bezpośrednio przez potok CI/CD.
* Izolacja: Pliki .env zawierające realne sekrety zostały wyłączone z systemu kontroli wersji (git) zgodnie z dobrymi praktykami bezpieczeństwa.

---
