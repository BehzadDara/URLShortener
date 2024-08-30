using FluentValidation;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using URLShortener.API.Configs;
using URLShortener.Application;
using URLShortener.Application.Behaviours;
using URLShortener.Application.Configs;
using URLShortener.Domain.Enums;
using URLShortener.Domain.Repositories;
using URLShortener.Infrastructure;
using URLShortener.Infrastructure.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

builder.Services.AddControllers();

builder.Services.AddLocalization();

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

    c.OperationFilter<AddAcceptLanguageHeaderParameter>();

    c.EnableAnnotations();
});

builder.Services.AddHealthChecks().AddDbContextCheck<URLShortenerDBContext>("Database HealthCheck");

var app = builder.Build();

var supportedCultures = Enum
    .GetValues(typeof(Languages))
    .Cast<Languages>()
    .Select(x => x.ToString())
    .ToArray();

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();
app.MapHealthChecks("/healthz", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

app.Run();
