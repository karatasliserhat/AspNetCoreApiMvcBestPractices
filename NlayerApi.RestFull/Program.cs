using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NlayerApi.Core.DTOs;
using NlayerApi.Core.IRepositories;
using NlayerApi.Core.IServices;
using NlayerApi.Core.UnitOfWork;
using NlayerApi.Repository.Context;
using NlayerApi.Repository.GenericRepositories;
using NlayerApi.Repository.UnitOfWorks;
using NlayerApi.RestFull.CustomException;
using NlayerApi.RestFull.Filters;
using NlayerApi.RestFull.Modules;
using NlayerApi.Service.Mappings;
using NlayerApi.Service.Services;
using NlayerApi.Service.Validations;
using System.Reflection;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(opts => opts.Filters.Add(new ValidationActionFilter())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductValidation>());

        builder.Services.AddAutoMapper(typeof(MapperProfile));
        builder.Services.AddScoped(typeof(NotFoundActionFilter<>));
        //api servisler kendi filterlarımızı algılaması için varsayılan modelstate filterine kapatmamanız gerekiyor
        builder.Services.Configure<ApiBehaviorOptions>(opts => { opts.SuppressModelStateInvalidFilter = true; });

        builder.Services.AddDbContext<AppDbContext>(opts =>
        {

            opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
            {
                options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
            });
        });


        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(conf => conf.RegisterModule(new RepoServiceModule()));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.CustomExceptionError();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}