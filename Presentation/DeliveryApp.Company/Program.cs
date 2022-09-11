using DeliveryApp.Infrastructure;
using DeliveryApp.Persistence;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;


builder.Services.AddControllersWithViews();
builder.Services.AddPersistenceServices(Configuration);
builder.Services.AddInfrastructureServices();
var app = builder.Build();

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
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
