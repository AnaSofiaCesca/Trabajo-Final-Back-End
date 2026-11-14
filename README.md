# Trabajo Final – Desarrollo de Back End

**Integrantes:**
- Ana Sofía Cesca
- Dante Cesca

**Tema:** Turnero Médico (Doctor, Paciente, Turno)

## Stack
- ASP.NET Core MVC (.NET 10)
- EF Core 10.0.15 + SQL Server LocalDB
- AutoMapper 16.3.0

## Arquitectura
| Proyecto | Capa | Contenido |
|---|---|---|
| TpFinal.Core | Dominio | Entidades, enums e interfaces (sin dependencias) |
| TpFinal.DL | Acceso a datos | AppDbContext, repositorios, Unit of Work, migraciones |
| TpFinal.BL | Lógica de negocio | DTOs, MappingProfile, servicios |
| TpFinal.Web | Presentación (UI) | Controllers, ViewModels, vistas Razor |

## Cómo correrlo
dotnet restore
dotnet ef database update --project TpFinal.DL --startup-project TpFinal.Web
dotnet run --project TpFinal.Web

## Flujo de trabajo
- No se hace push directo a main.
- Una rama por tarea: feature/xxx → Pull Request → revisión → merge.
- Las migraciones las crea una sola persona por vez.
