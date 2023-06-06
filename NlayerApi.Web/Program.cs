using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NlayerApi.Repository.Context;
using NlayerApi.Service.Mappings;
using NlayerApi.Service.Validations;
using NlayerApi.Web.Filters;
using NlayerApi.Web.Modules;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(opts => opts.RegisterValidatorsFromAssemblyContaining<ProductValidation>());


builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddDbContext<AppDbContext>(opts =>
{


    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
    {

        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(conf => conf.RegisterModule(new RepoServiceModule()));
var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
