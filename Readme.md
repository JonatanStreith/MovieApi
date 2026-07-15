# MovieApi

This is a multipurpose API framework connecting to a database storing information about movies, set up for study purposes. It incorporates various API strategies, such as Clean Architecture, versioning, etc.

## LAYERS (Clean Architecture)
MovieApi uses seven layers, stored in separate projects, for easy separation of concerns. Each layer handles one area of the application.

### MovieApi:
The primary functional part of the API, this part holds the main executable, the appsettings, and most importantly, the controllers.
References: Data, Presentation, Services.

### Services:
The services hold various functions that can call on the repositories to fetch and send data. Currently they don't do much with said data besides pass it on to the controllers, and only exist due to API standards. However, later on they may have greater functionality. All services are gathered under the ServiceManager.
References: Contracts.

### Data:
This part manages the database context, the configurations of the database (including seed data when setting up a new database), and the migrations. It holds the essential repositories used to access the database directly. All repositories are gathered under UnitOfWork.
References: Core.

### Presentation:
This layer contains all frontend data. As no frontend has been built yet, it remains empty and vacant.
References: Contracts.

### Core:
Core keeps all the classes for representing data. This includes the database entities like Movie and Actor, the data transfer objects, and interfaces for the repositories.
References: None.

### Contracts:
This layer holds the contracts (interfaces) for the services.
References: Core.

### Test:
Used for testing the continuous functionality of the API. Technically not part of the API system for purposes of running it.

## Additional functions:
MovieApi includes multiple extra functions as part of an exercise in modern API design.

### Versioning:
The controllers are built to support versioning, allowing for multiple layers of endpoints so the developers can build new endpoints while keeping old ones that may still be used.

### Fluent API:
The database configuration is written in a simple and readable standard, and separated into individual configuration files. As the system is set to simply read all available configs, making new additions is just a matter of dropping in another config file.

### JWT authorization:
The API has a basic implementation of authorization, with a login controller and  a secure data controller that require the user to be logged in to access. While only having the bare bones, it can be expanded upon and/or used as reference for fullscale security in other APIs.

### Swagger:
The API includes Swagger for endpoint testing.
