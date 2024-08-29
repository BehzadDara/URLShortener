using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using URLShortener.Application;
using URLShortener.Application.Behaviours;
using URLShortener.Domain.Repositories;
using URLShortener.Infrastructure;
using URLShortener.Infrastructure.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IURLRepository, URLRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<URLShortenerDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(URLShortenerMapper));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssemblyContaining(typeof(URLShortenerMapper));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "URL Shortener",
        Contact = new OpenApiContact
        {
            Name = "Behzad Dara",
            Email = "Behzad.Dara.99@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/behzaddara/")
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
