using HajjSystem.Data;
using HajjSystem.Data.Repositories;
using HajjSystem.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext - using InMemory for now (no connection string provided)
// builder.Services.AddDbContext<HajjSystemContext>(options =>
//     options.UseInMemoryDatabase("HajjSystemDb"));

// Configure DbContext - using PostgreSQL
builder.Services.AddDbContext<HajjSystemContext>(option =>
option.UseNpgsql(builder.Configuration.GetConnectionString("HajjSystemConnectionString")));


// Repositories & services
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

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
