using Microsoft.EntityFrameworkCore;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Repositories;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Repositories;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OnlineShopDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
        ));
builder.Services.AddScoped<IPersonRepository<Person, Guid?>, PersonRepository<Person, Guid?>>();
builder.Services.AddScoped<IProductRepository<Product, Guid?>, ProductRepository<Product, Guid?>>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
