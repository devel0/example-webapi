# example web api

- [features](#features)
- [quickstart](#quickstart)
- [graceful shutdown](#graceful-shutdown)
- [json camelcase](#json-camelcase)
- [seealso](#seealso)
- [how this project was built](#how-this-project-was-built)

## features

same as `dotnew new webapi` plus:
- serilog
- swagger ( dark theme ) with xml generated doc from code and custom document filter to add more custom types
- injectable services into controllers
- lifetime with cts to deal with graceful shutdown
- code casing extensions and util service for json camelcase configuration

## quickstart

- install repo as template

```sh
git clone https://github.com/devel0/example-webapi.git
cd example-webapi
dotnet new install .
cd ..
```

- create project from template

```sh
dotnet new webapi-skel -n project-folder --namespace My.Some
cd project-folder
dotnet run
```

## graceful shutdown

```sh
devel0@mini:~/opensource/example-webapi$ dn run
[19:12:49 INF] Backend application started
[19:12:49 INF] Application started
[19:12:49 INF] Environment: Production
[19:12:49 INF] Listening on http://localhost:5000
^C
[19:12:50 INF] Backend application stopping
[19:12:50 INF] Backend application stopped
[19:12:50 INF] Application stopping
[19:12:50 INF] Graceful shutdown in progress
[19:12:50 INF] Fake 3 sec wait
[19:12:53 INF] Graceful shutdown completed
[19:12:53 INF] Application stopped
```

## json camelcase

injecting `IUtilService` there is a ready to use `JavaSerializerSettings` method to pass a configured json options object:

```csharp
var obj = JsonSerializer.Deserialize<Nfo>(res, util.JavaSerializerSettings());
```

## seealso

for a more advanced scenario [example-webapi-with-auth](https://github.com/devel0/example-webapp-with-auth)

## how this project was built

```sh
dotnet new webapi -n ExampleWebApi -f net9.0 -controllers --exclude-launch-settings
cd ExampleWebApi
dotnet new gitignore
dotnet add package Serilog.Extensions.Hosting --version 9.0.0
dotnet add package Serilog.Settings.Configuration --version 9.0.0
dotnet add package Serilog.Sinks.Console --version 6.0.0
dotnet add package Swashbuckle.AspNetCore --version 7.2.0
dotnet add package Swashbuckle.AspNetCore.Annotations --version 7.2.0
dotnet add package Unchase.Swashbuckle.AspNetCore.Extensions --version 2.7.2
```