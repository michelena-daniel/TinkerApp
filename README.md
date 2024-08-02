# TinkerApp

## Tech Stack:<a name="tech-Stack"/>

- .NET 8 following MVC pattern
- EF Core with Identity to handle persistence, users and DB migrations
- Docker to add orchestration support and containerize tools on compose.yaml
- Postgres 16 image as DB
- Bootstrap 5 for UI design
- Chart.js via CDN to manage graph drawing
- SendGrid to manage mail.

## How to RUN (requires Docker):

- Download/Clone the project from GitHub
```
https://github.com/michelena-daniel/TinkerApp.git
```
- Run docker compose to spin up the containers (from root directory where the compose .yml is located):
```
docker compose up
```
- Check on which host ports the containers are running:
```
docker ps
```
You can now open the application on your localhost, but any login or register attempt will fail since you still need to apply migrations

- Apply migrations:
  - First you'll need .NET CLI access:
  ```
  dotnet tool install --global dotnet-ef --version 8.*
  ```
  - Second you'll have to navigate to the appsettings.json file and change the DB host (since it is mapped to a docker host). Change (...)"DefaultConnection": "Host=tinkerdb;(...) to:
  ```
  "DefaultConnection": "Host=localhost;
  ```
  - Now you can access DB directly and can apply migrations from project directory running:
  ```
  dotnet ef database update
  ```
  - Once migrations are applied you can change the appsettings back to the docker host and spin it up again when needed using docker commands.

## External Login using Google: <a name="google"/>

Google external login is setup and ready, but it'll need a generated ClientID on their side to fill the appsettings.json:
```
"GoogleClient": {
    "ClientId": "clientidplaceholder",
    "ClientSecret": "clientsecretplaceholder"
  }
```
You can create a new client on: https://developers.google.com/identity/sign-in/web/sign-in


  


