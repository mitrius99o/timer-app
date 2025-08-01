# ⏰ Timer App

Веб-приложение для создания и управления таймерами с функциями запуска, остановки и редактирования времени.

## 🚀 Функции

- ✅ Создание таймеров с настраиваемым временем
- ✅ Запуск и остановка таймеров
- ✅ Редактирование времени таймеров
- ✅ Сброс таймеров
- ✅ Удаление таймеров
- ✅ Отображение прогресса в реальном времени
- ✅ Современный и отзывчивый интерфейс

## 🛠 Технологии

- **Backend:** ASP.NET Core 9.0
- **Frontend:** HTML, CSS, JavaScript
- **Архитектура:** REST API

## 📦 Установка и запуск

### Локальная разработка

1. Клонируйте репозиторий:
```bash
git clone <your-repo-url>
cd TimerApp
```

2. Запустите приложение:
```bash
dotnet run
```

3. Откройте браузер и перейдите по адресу: `http://localhost:5229`

## 🌐 Деплой

### Railway (Рекомендуется)

1. Создайте аккаунт на [railway.app](https://railway.app)
2. Подключите ваш GitHub репозиторий
3. Railway автоматически определит .NET приложение и задеплоит

### Render

1. Создайте аккаунт на [render.com](https://render.com)
2. Создайте новый Web Service
3. Подключите GitHub репозиторий
4. Выберите .NET как runtime

### Heroku

1. Установите Heroku CLI
2. Выполните команды:
```bash
heroku create your-timer-app
git push heroku main
```

## 📁 Структура проекта

```
TimerApp/
├── Controllers/          # API контроллеры
├── Models/              # Модели данных
├── Services/            # Бизнес-логика
├── wwwroot/            # Статические файлы
│   └── index.html      # Главная страница
├── Program.cs          # Точка входа
└── TimerApp.csproj     # Файл проекта
```

## 🔧 API Endpoints

- `GET /api/timer` - Получить все таймеры
- `GET /api/timer/{id}` - Получить таймер по ID
- `POST /api/timer` - Создать новый таймер
- `POST /api/timer/{id}/start` - Запустить таймер
- `POST /api/timer/{id}/stop` - Остановить таймер
- `POST /api/timer/{id}/reset` - Сбросить таймер
- `PUT /api/timer/{id}` - Обновить таймер
- `DELETE /api/timer/{id}` - Удалить таймер

## 📝 Лицензия

MIT License 