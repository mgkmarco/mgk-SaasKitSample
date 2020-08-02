# SaasKitSample

## Build and Run

You can build and run the application using one of the following methods.

### Docker (same as build server) - preferred

```
cd docker
docker-compose up --build
```

### Dotnet CLI
```
dotnet run --project src/SaasKitSample.Host/SaasKitSample.Host.csproj
```

## Tests
You can run all the integration and unit tests using the following commands:

### Docker (same as build server) - preferred

```
# Build solution
docker build . -t gig-core:test --target test
# Copy Artifacts
docker run --rm gig-core:test
```

### Dotnet CLI

```
dotnet test --filter "Category=Unit|Category=Integration|Category=Component" SaasKitSample.sln 
```