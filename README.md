# Trabajo Final – Desarrollo de Back End

**Integrantes:** Nombre 1, Nombre 2
**Tema:** (a definir)

## Stack
- ASP.NET Core MVC (.NET 10)
- EF Core 10.0.15 + SQL Server LocalDB
- AutoMapper 14.0.0 (riesgo aceptado CVE-2026-32933, uso académico/local; ver .csproj)

## Cómo correrlo
dotnet restore
dotnet ef database update --project TpFinal.Web
dotnet run --project TpFinal.Web

## Flujo de trabajo
- No se hace push directo a main.
- Una rama por tarea: feature/xxx → Pull Request → revisión → merge.
- Las migraciones las crea una sola persona por vez.
