## CarShowroomApp
Angular 9 Web application with .Net Core 3.1 RESTful Web API backend.

Application built to manage the Car showroom.

## RESTful WEB API
- Authentication and Authorization (JWT)
- Clean Architecture
- Logging (Log4Net)
- Users with Roles
- Dtos and Mapper
- Distributed Caching - Redis
- Unit Tests
- SignalR - real-time chat
- DI with AutoFac
- Swagger

It is a Full-stack project. I started from creating the Web API in .Net Core 3.1. I used Entity Framework Core (Code first approach) to generate database with migrations. 
API has its own validation (Data annotations) and it's transfering data via DTO objects in JSON format. I used Domain, Application, Infrastructure, UI architecture to impart business logic from the data mapping layer. In the repository entities are mapped to proper (DTO or original) objects with AutoMapper. Operations in API are being executed asynchronously and GET requests are being cached in Redis. API has Unit Tests (XUnit) using InMemory database. Whole project uses Log4Net for File, Console and Debug logging. Thanks to SignalR API provides Users with real-time chat with JWT authentication.

## Angular 9 GUI

I created GUI with Angular 9. HttpService is responsible for communicating with API. OverviewComponent is the main page where we can see the list of all cars in database. Data is fetched using API. AddComponent allows (Reactive Form) adding new cars to the database and it also validates data by showing alerts (Default validators like 'required', 'min' and 'max').

Summary, what this project contains:
- .Net Core 3.1 / C#
- Entity Framework Core (Code First)
- RESTful WEB API
- .NET Core Identity
- Angular 9 (AppRouting, HttpClient, ReactiveForms)
- Unit Tests (XUnit)
- SignalR real-time chat
- Angular Material Design
- TypeScript
- CSS
- HTML
