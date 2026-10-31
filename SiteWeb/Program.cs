using Microsoft.EntityFrameworkCore;
using SiteWeb.Application.Interfaces;
using SiteWeb.Application.Mappings;
using SiteWeb.Application.Services;
using SiteWeb.Infrastructure;
using SiteWeb.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add MVC
builder.Services.AddControllersWithViews();

// ✅ AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// ✅ Infrastructure services (DbContext, Identity, Repositories, FileStorageService)
builder.Services.AddInfrastructureServices(builder.Configuration);

// ✅ Application services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

// ✅ Auto-apply pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
