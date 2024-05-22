using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.Repositories;
using Newtonsoft.Json.Serialization;

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


builder.Services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
