using AuroraUniversity.Application; // اگر متد AddApplicationServices اینجا تعریف شده
using AuroraUniversity.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// اضافه کردن DbContext
builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// اضافه کردن سرویس‌ها و ریپازیتوری‌ها از لایه Application
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

// اضافه کردن کنترلرها و Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// فعال کردن Swagger در محیط توسعه
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuroraUniversity API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.Run();
