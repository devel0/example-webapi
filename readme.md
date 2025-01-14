# example web api

- [features](#features)
- [seealso](#seealso)
- [how this project was built](#how-this-project-was-built)

## features

same as `dotnew new webapi` plus:
- serilog
- swagger ( dark theme ) with xml generated doc from code and custom document filter to add more custom types
- injectable services into controllers
- lifetime with cts to deal with graceful shutdown

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