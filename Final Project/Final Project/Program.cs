using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Final_Project;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




var conString = builder.Configuration.GetConnectionString("Default") ??
     throw new InvalidOperationException("Connection string 'Default'" +
    " not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conString));

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
                                            .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.SignIn.RequireConfirmedEmail = true;

    options.User.RequireUniqueEmail = true;
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    // .WriteTo.Seq("http://localhost:5341") 
    .Enrich.FromLogContext()
    .CreateLogger();


builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<IFeaturedBookService, FeaturedBookService>();
builder.Services.AddScoped<IBestSellingBookService, BestSellingBookService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IContactMessageService, ContactMessageService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();


builder.Host.UseSerilog();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedRoles(roleManager);
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandler>();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
