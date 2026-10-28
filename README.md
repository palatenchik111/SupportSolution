# SupportSolution
Учебная система учета заявок технической поддержки.

## Стек технологий
- .NET 9
- C# 13
- xUnit
- Git

## Структура модулей
- `Support.Domain` — ядро, сущности и бизнес-правила.
- `Support.Application` — контракты и интерфейсы.
- `Support.Infrastructure` — технические реализации (in-memory хранилище).
- `Support.WebApi` — HTTP API.

## Команды
- Сборка: `dotnet build`
- Тесты: `dotnet test`
- Запуск API: `dotnet run --project src/Support.WebApi`