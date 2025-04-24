# ToDo Test Project

This is a Clean Architecture sample project in ASP.NET Core.
The domain of this project is super simple but DDD principals and CQRS pattern are followed into it.

# How To Run?

It uses MSSQL Server on docker. To run on docker it's just run `docker-compose up -d`.

The database will be created automatically. It will be available via `http://localhost:6480`.

The project could be run without docker only if you have MSSQL on your system and the proper connection string in
`appsettings.Development.json`.

# Endpoints Playground

[Scalar](https://github.com/scalar/scalar) is used as a Swagger UI alternative: `http://localhost:6480/scalar`.

💡It is disabled on docker because it is considered as `Production` environment.

# Endpoints

| Endpoint           | Description                          |
|--------------------|--------------------------------------|
| GET /todo          | Get list of ToDos with pagination    |
| GET /todo/{id}     | Get details of a ToDo by Id          |
| POST /todo         | Create a new ToDo                    |
| PUT /todo          | Update a ToDo information            |
| PUT /todo/activate | Change status of a ToDo to activated |
| PUT /todo/done     | Change status of a ToDo to done      |
| DELETE /todo/{id}  | Delete a ToDo                        |

# Validation

[Mediator](https://github.com/martinothamar/Mediator) is used in this project. All commands and queries will be
validated via a Pipeline Behavior of Mediator. A proper response is generated for the invalid requests.

# Exception Handling

Another Pipeline Behavior is used for handling all exceptions globally.
Also, an exception handler to handling all other API level exceptions and generating uniform
response is used via `UseExceptionHandler` extension method.

# Caching

Memory Cache provider is used for caching ToDo items in `GET /todo/{id}` endpoint.
The cached values will be updated by handling the domain events that published after modifying Todos. It is used only in
production environment.

# Unit Tests

Unit tests is written for following assets:

- Aggregates
- Validators

# Integrated Tests

Each command/query handler has an integrated test class. EF Core in memory provider is used to persisting data during
tests.
