using Microsoft.EntityFrameworkCore;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.PersonAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.DomainModels.ProductAggregates;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Contracts;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services.Repositories;
using RepositoryDesignPatternDomainDrivenDesign.Models.Services;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.PersonDtos;
using RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.PersonDtos;
using AutoMapper;
using RepositoryDesignPatternDomainDrivenDesign.Controllers.Dtos.ProductDtos;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Dtos.ProductDtos;
using RepositoryDesignPatternDomainDrivenDesign.Controllers;
using RepositoryDesignPatternDomainDrivenDesign.ApplicationServices.Services;
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
builder.Services.AddScoped<PersonService, PersonService>();
builder.Services.AddScoped<ProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(Program));

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<InsertPersonDtoController, InsertPersonDtoService>();
    cfg.CreateMap<DeletePersonDtoPostController, DeletePersonDtoPostService>();
    cfg.CreateMap<UpdatePersonDtoGetController, UpdatePersonDtoGetService>();
    cfg.CreateMap<UpdatePersonDtoPostService, UpdatePersonDtoPostController>();
    cfg.CreateMap<UpdatePersonDtoPostController, UpdatePersonDtoPostService>();
    cfg.CreateMap<SelectPersonDtoController, SelectPersonDtoService>();

    cfg.CreateMap<InsertProductDtoController, InsertProductDtoService>();
    cfg.CreateMap<DeleteProductDtoPostController, DeleteProductDtoPostService>();
    cfg.CreateMap<UpdateProductDtoGetController, UpdateProductDtoGetService>();
    cfg.CreateMap<UpdateProductDtoPostService, UpdateProductDtoPostController>();
    cfg.CreateMap<UpdateProductDtoPostController, UpdateProductDtoPostService>();
    cfg.CreateMap<SelectProductDtoController, SelectProductDtoService>();

});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


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
