using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.Repositories;
using Newtonsoft.Json.Serialization;
using MVCSmallFarm.ViewModels;
using MVCSmallFarm.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SmallFarmContext>(

    option => option.UseSqlServer(builder.Configuration.GetConnectionString("SmallFarm"))
    );

//builder.Services.AddMvc().AddJsonOptions(options =>
//      {
//          options.JsonSerializerOptions.PropertyNamingPolicy = null;
//      })
//      .AddNewtonsoftJson(opts =>
//      {
//          opts.SerializerSettings.ContractResolver = null;

//      });


builder.Services.AddMemoryCache();  //Using session
builder.Services.AddSession();      //Using session

builder.Services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddSingleton<DBConnector>();   //Using DBConnector (class) in Blazor

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<CommentsService>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


//builder.Services.AddSingleton<IHttpContextAccessor, IHttpContextAccessor>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(sc => ShppingCartService.CreateShoppingCart(sc));

builder.Services.AddServerSideBlazor();  //Using Blazor component

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\img")),
    RequestPath = "/wwwroot/img"
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); //Using session ::must call before app.MapControllerRoute

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapBlazorHub();    //Using Blazor component

app.Run();
