using Microsoft.EntityFrameworkCore;
using newmvc.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer("server=MACHINA; database=db1; trusted_connection=true; TrustServerCertificate=True");
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5052); // Or use .Listen(IPAddress.Parse("192.168.1.100"), 5052)
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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "products",
    pattern: "{controller=Products}/{id?}")
    .WithStaticAssets();

app.UseStaticFiles();

app.Run();
