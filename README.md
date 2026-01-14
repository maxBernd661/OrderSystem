# OrderSystem

A sample application for an order/purchase order system based on .NET 8 with a Windows desktop app (WinForms), an ASP.NET Core Web API, and a shared core library. Data is stored using Entity Framework Core with SQLite.



## Project structure


- **OrderSystem.Win** – Windows Forms UI (desktop client).
- **OrderSystem.API** – ASP.NET Core Web API with Swagger.
- **OrderSystem.Core** – Shared domain logic and EF Core context/models.

The `OrderSystem.sln` solution is located in the `OrderSystem.Win` folder and references all three projects.

## Prerequisites

- .NET SDK 8.x
- For the WinForms app: Windows (net8.0-windows)


## Configuration


Both applications (API and WinForms) use an SQLite database. The path is stored in the respective `appsettings.json` files:


- `OrderSystem.API/appsettings.json`

- `OrderSystem.Win/appsettings.json`


Adapt the connection string to your environment, e.g.:


```json

“ConnectionStrings”: {

  “Default”: “Data Source=C:\\data\\ordersystem.db”

}

```


> Note: Migrations are applied automatically when the API is started.


## Starting the application


### Web API


```bash

dotnet run --project OrderSystem.API

```


By default, Swagger is active in development mode.


### Windows desktop app


```bash

dotnet run --project OrderSystem.Win

```


Alternatively, you can open the `OrderSystem.Win/OrderSystem.sln` in Visual Studio and start/debug it from there.


## Database & Migrations (optional)


If you want to create your own migrations:


```bash

# In the Core project folder

dotnet ef migrations add <MigrationName> --project ../OrderSystem.Core --startup-project ../OrderSystem.API


dotnet ef database update --project ../OrderSystem.Core --startup-project ../OrderSystem.API

```


## Notes


- The API and the WinForms app share the same data model from `OrderSystem.Core`.

- Make sure that both applications point to the same SQLite file if you want to share the data.
