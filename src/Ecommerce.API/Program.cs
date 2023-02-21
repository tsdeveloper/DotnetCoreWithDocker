using Ecommerce.API.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var serviceProvider = builder.Services.BuildServiceProvider();
var config = serviceProvider.GetService<IConfiguration>();
builder.Services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(config.GetConnectionString("DockerSQLServerLocal")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateAsyncScope();
var services = scope.ServiceProvider;
var context = services.GetService<DatabaseContext>();
await context.Database.MigrateAsync();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
