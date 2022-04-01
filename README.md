# MongoDB Factory for .NET

This project provides a factory extension for the MongoDB driver to use with .NET dependency injection.

> This is still a work in progress. Things might change, break or disappear. Use at your own risk.

## Getting Started

Install the package from NuGet

```bash
dotnet add package MongoDB.Factory
```

Register the factory with the DI container

```csharp
IServiceCollection services = ...;

services.AddMongoClient( connectionString );
```

Inject the factory instance in your code and retrieve the client instance.

```csharp
public class MyService
{
    private readonly IMongoClient mongoClient;

    public MyService( IMongoFactory mongoFactory )
    {
        mongoClient = mongoFactory.CreateClient();
    }
}
```
