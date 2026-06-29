using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Persistence;
using TodoApi.Application.Interfaces;
using TodoApi.Infrastructure.Repositories;
using TodoApi.Application.CommandHandlers;
using TodoApi.Application.QueryHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

builder.Services.AddScoped<GetAllToDosHandler>();
builder.Services.AddScoped<GetToDoByIdHandler>();
builder.Services.AddScoped<CreateToDoHandler>();
builder.Services.AddScoped<UpdateToDoHandler>();
builder.Services.AddScoped<DeleteToDoHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.MapControllers();

app.Run();