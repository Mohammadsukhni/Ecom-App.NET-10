using Ecom.Api.Mapping;
using Ecom.Api.Middleware;
using Ecom.Infrastructure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InfrastructureConfiguration(builder.Configuration);
builder.Services.AddAutoMapper(_ => { }, AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithRedirects("/errors/{0}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
