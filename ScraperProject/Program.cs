using Microsoft.EntityFrameworkCore;
using ScraperProject;
using ScraperProject.Database;
using ScraperProject.Services;
using ScraperProject.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("ScraperDb"));
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<ILaptopService, LaptopService>();
builder.Services.AddScoped<WebScraper>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddHostedService<BackgroundScraper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    DatabaseSeeder.Seed(databaseContext);
}

app.Run();
