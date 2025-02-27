using EmployeeMaintenance.Application.Contracts;
using EmployeeMaintenance.Application.Handlers;
using EmployeeMaintenance.Infrastructure.Common;
using EmployeeMaintenance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg =>
{
    _ = cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeHandler).Assembly);
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "X-API-KEY",
        Type = SecuritySchemeType.ApiKey,
        Description = "API Key authentication"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        _ = builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
WebApplication app = builder.Build();
app.UseCors("AllowReactApp");
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
    _ = app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Use(async (context, next) =>
{
    string? apiKey = context.Request.Headers["X-API-KEY"].FirstOrDefault();
    if (apiKey != "secret-key")
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid API Key");
        return;
    }
    await next();
});
app.Run();