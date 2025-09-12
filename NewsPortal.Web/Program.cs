using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Application.Interfaces;
using NewsPortal.Application.Services;
using NewsPortal.Infrastructure.Data;
using NewsPortal.Infrastructure.Interfaces;
using NewsPortal.Infrastructure.Repositories;
using NewsPortal.Infrastructure.Seeder;
using NewsPortal.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AuthService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddScoped<ITagRepository, TagRepositoryImpl>();
builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<IFileService, FileServiceImpl>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<IUserRepository,UserRepositoryImpl>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ICurrentUser, CurrentUserService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    UserSeeder.SeedUsers(db);
    TagSeeder.SeedTags(db);
    CategorySeeder.SeedCategories(db);
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();