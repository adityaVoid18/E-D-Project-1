using E_D_Project_1.Models;
using E_D_Project_1.Repository;
using E_D_Project_1.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEdRepository, EdRepository>();
builder.Services.AddScoped<IEdService, EdService>();

builder.Services.AddDbContext<edDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));

// Add services to the container.

builder.Services.AddControllers();
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
