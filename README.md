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
    `docker-compose up`
4.  Po poprawnym uruchomieniu kontenerów, interfejs Swagger będzie dostępny pod adresem:
    `http://localhost:8080/swagger/index.html`

## Konfiguracja i bezpieczeństwo
W pliku docker-compose.yml zostały zawarte domyślne dane uwierzytelniające (hasła i loginy do bazy danych). 

Ważne informacje dotyczące bezpieczeństwa:
* Uproszczona konfiguracja: Hasła zostały wpisane na sztywno w pliku konfiguracyjnym wyłącznie w celu umożliwienia natychmiastowego uruchomienia projektu przez osobę rekrutującą.
* Standardy produkcyjne: W środowisku produkcyjnym dane wrażliwe byłyby przechowywane z wykorzystaniem mechanizmów takich jak Docker Secrets, Azure Key Vault lub zmienne środowiskowe wstrzykiwane bezpośrednio przez potok CI/CD.
* Izolacja: Pliki .env zawierające realne sekrety zostały wyłączone z systemu kontroli wersji (git) zgodnie z dobrymi praktykami bezpieczeństwa.

---
