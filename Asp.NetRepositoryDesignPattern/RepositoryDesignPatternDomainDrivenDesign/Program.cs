using Microsoft.EntityFrameworkCore;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Repositories;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;

using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services.Contracts;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.ProductDtos;
using Microsoft.AspNetCore.Hosting;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextPool<OnlineShopDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
    ));
builder.Services.AddScoped<IPersonRepository<Person, Guid?>, PersonRepository<Person, Guid?>>();
//builder.Services.AddScoped<IProductRepository<Product, Guid?>, ProductRepository<Product, Guid?>>();
builder.Services.AddScoped<IPersonApplicationService<SelectPersonDtoService, SelectPersonDtoService, SelectPersonDtoService, InsertPersonDtoService, DeletePersonDtoPostService, UpdatePersonDtoPostService, CreateAbstractIdPersonDtoService, GetRealIdPersonDtoService>, PersonService>();





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
    pattern: "{controller=Person}/{action=Person}/{id?}");

app.Run();
