
using Application.Extensions;
using Application.RequestHelpers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data.SeedData;
using Infrastructure.Extensions;
using Skinet.Middlewares;
using Skinet.Registrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddApiServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

var app = builder.Build();
var scope =app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IProductSeeder>();
await seeder.SeedDataAsync();
app.UseMiddleware<ErrorHandelingMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().AllowAnyOrigin().WithOrigins("http://localhost:4200", "https://localhost:4200"));



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
