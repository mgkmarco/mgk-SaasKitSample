# SaasKitSample

## Build and Run

You can build and run the application using one of the following methods.

### Docker (same as build server) - preferred

```
cd docker
docker-compose up -d --force-recreate
```

### Dotnet CLI

```
dotnet test --filter "Category=Unit|Category=Integration|Category=Component" SaasKitSample.sln 
```

### Demoing the application
```
- In the swagger hit the main API... which is hosted at localhost:4000/api-docs
- Leave the X-Tenant-ID blank... and this should return a response from the DEFAULT tenant
- Put "Foo" as X-Tenant-ID... and this should return a response from the FOO tenant
- Put "Bar" as X-Tenant-ID... and this should return a response from the Bar tenant

If you find any issues please let me know. Keep in mind that the tenant is cached, therefore in the middleware it is going to be resolved only once (until the cahce is evicted)
```
