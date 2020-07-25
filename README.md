## CarShowroomApp
Angular 9 Web application with .Net Core 3.1 Web API backend

Application built to manage the Car showroom.

It is a Full-stack project. I started from creating the Web API in .Net Core 3.1. I used Entity Framework (Code first approach) to generate database. API has its own validation (Data annotations) and it's transfering data via DTO objects. I used "Repository design pattern" to impart business logic from the data mapping layer (ICarRepository interface).

I created GUI with Angular 9. HttpService is responsible for communicating with API. OverviewComponent is the main page where we can see the list of all cars in database. Data is fetched using API. AddComponent allows (Reactive Form) adding new cars to the database and it also validates data by showing alerts (Default validators like 'required', 'min' and 'max').

Summary, what I have used:
- .Net Core 3.1 / C#
- Entity Framework Core (Code First)
- WEB API
- Angular 9 (AppRouting, HttpClient, ReactiveForms)
- Angular Material Design
- TypeScript
- CSS
- HTML
