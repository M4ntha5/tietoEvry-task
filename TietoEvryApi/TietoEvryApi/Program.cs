using Microsoft.EntityFrameworkCore;
using TietoEvry.Core;
using TietoEvry.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("TietoEvryDB"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAutoMapper(typeof(Program).Assembly,
    typeof(TietoEvry.Core.Workouts.AutoMapperProfiles.WorkoutsProfile).Assembly,
    typeof(TietoEvry.Core.Exercises.AutoMapperProfiles.WorkoutsProfile).Assembly);

builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddEndpoints();

app.Run();