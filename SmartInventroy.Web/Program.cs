using Microsoft.EntityFrameworkCore;
using SmartInventroy.BLL;
using SmartInventroy.BLL.Service;
using SmartInvetory.DAL;
using SmartInvetory.DAL.EF;
using SmartInvetory.DAL.Interfaces;
using SmartInvetory.DAL.Repository;

var builder=WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SmartInventoryDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<DataAccessFactory>();
builder.Services.AddScoped<IOperationsRepository, OperationsRepository>();
builder.Services.AddScoped<OperationsService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<WarehouseService>();

var app=builder.Build();
if(!app.Environment.IsDevelopment()){app.UseExceptionHandler("/Home/Error");app.UseHsts();}
app.UseHttpsRedirection();app.UseRouting();app.UseAuthorization();app.MapStaticAssets();
app.MapControllerRoute(name:"default",pattern:"{controller=Home}/{action=Index}/{id?}").WithStaticAssets();
app.Run();
