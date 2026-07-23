using Microsoft.EntityFrameworkCore;
using FikirHavuzu.Services;
using FikirHavuzu.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddScoped<IKullaniciService, KullaniciService>();
builder.Services.AddScoped<IFikirRepository, FikirRepository>();
builder.Services.AddScoped<IFikirService, FikirService>();
builder.Services.AddScoped<IYetkiRepository, YetkiRepository>();
builder.Services.AddScoped<IYetkiService, YetkiService>();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FikirHavuzuDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddAuthentication("FikirHavuzuAuth")
    .AddCookie("FikirHavuzuAuth", options =>
    {
        options.LoginPath = "/Auth/Giris";
        options.AccessDeniedPath = "/Auth/YetkiYok";
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
    
app.Run();
