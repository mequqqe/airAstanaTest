AirAstanaService — это веб-API для управления статусами рейсов. Оно позволяет клиентам аутентифицироваться, получать информацию о рейсах и выполнять CRUD-операции с данными о рейсах в зависимости от их уровня доступа. Проект построен с использованием .NET 6, следует принципам Domain-Driven Design, включает кэширование с Redis, логирование с помощью Serilog и использует MediatR для реализации паттернов CQRS и Mediator.

## Возможности
- Аутентификация пользователей с помощью JWT токенов.
- CRUD-операции с данными о рейсах.
- Управление доступом на основе ролей (Модератор, Пользователь).
- Кэширование с использованием Redis.
- Логирование с использованием Serilog.
- Документация API с помощью Swagger.

## Технологии
- .NET 6
- Entity Framework Core
- MediatR
- FluentValidation
- Serilog
- Redis
- Swagger

## Структура проекта
- **Domain**: Содержит основные сущности и перечисления.
- **Application**: Содержит интерфейсы сервисов, DTO, команды, запросы и обработчики.
- **Infrastructure**: Содержит реализации репозиториев, контекст базы данных и логирование.
- **Presentation**: Содержит контроллеры и middleware.

### Настройка
1. Клонируйте репозиторий:
    ```bash
    git clone https://github.com/mequqqe/airAstanaTest.git
    cd airAstanaService
    ```

2. Обновите файл `appsettings.json` с вашими настройками базы данных и Redis:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Database=airastana;Username=postgres;Password=ваш_пароль",
        "Redis": "localhost:6379"
      },
      "Jwt": {
        "Key": "your_jwt_secret_key",
        "Issuer": "yourdomain.com",
        "Audience": "yourdomain.com"
      }
    }
    ```

3. Примените миграции базы данных:
    ```bash
    dotnet ef database update --project ./Infrastructure/Infrastructure.csproj
    ```

4. Запустите приложение:
    ```bash
    dotnet run --project ./Presentation/Presentation.csproj
    ```

### Тестирование
Для запуска юнит-тестов:
```bash
dotnet test --project ./Tests/Tests.csproj
API Endpoints

Аутентификация
POST /api/Auth/Login: Аутентификация и получение JWT токена.
Рейсы
GET /api/Flight: Получение списка рейсов.
POST /api/Flight: Добавление нового рейса (требуется роль Модератора).
PUT /api/Flight/{id}: Обновление существующего рейса (требуется роль Модератора).
DELETE /api/Flight/{id}: Удаление рейса (требуется роль Модератора).
