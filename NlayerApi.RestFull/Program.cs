using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NlayerApi.Core.IRepositories;
using NlayerApi.Core.IServices;
using NlayerApi.Core.UnitOfWork;
using NlayerApi.Repository.Context;
using NlayerApi.Repository.GenericRepositories;
using NlayerApi.Repository.UnitOfWorks;
using NlayerApi.RestFull.Filters;
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

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddAutoMapper(typeof(MapperProfile));

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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}