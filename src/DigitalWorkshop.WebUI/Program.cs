using DigitalWorkshop.Application.Services;
using DigitalWorkshop.Infrastructure.Data;
using DigitalWorkshop.Infrastructure.Services; // Добавлено
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Подключение БД
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация сервиса
builder.Services.AddScoped<ITechnologyProcessService, TechnologyProcessService>();

builder.Services.AddControllersWithViews(); // Заменили AddRazorPages

var app = builder.Build();

// ... остальной код (pipeline)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Заменили MapRazorPages на маппинг контроллеров
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();