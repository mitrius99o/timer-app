using TimerApp.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        // Убираем строку с JsonNamingPolicy, так как она не поддерживается в данной версии .NET
    });

builder.Services.AddSingleton<TimerService>();

// Настраиваем CORS для веб-интерфейса
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Настраиваем middleware
app.UseCors();
app.UseStaticFiles(); // Для обслуживания статических файлов из wwwroot

// Настраиваем маршрутизацию
app.MapControllers();

// Перенаправляем корневой путь на index.html
app.MapGet("/", () => Results.Redirect("/index.html"));

app.Run();
