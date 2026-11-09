using Microsoft.EntityFrameworkCore;
using SmartInventroy.BLL;
using SmartInventroy.BLL.Service;
using SmartInvetory.DAL;
using SmartInvetory.DAL.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SmartInventoryDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<DataAccessFactory>();



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


app.Run();
