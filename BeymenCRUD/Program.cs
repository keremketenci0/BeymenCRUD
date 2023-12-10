using BeymenCrud.Services;
using BeymenCRUD.Data;
using BeymenCRUD.Data.UserRepo;
using BeymenCRUD.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// #######################################################################

// Retrieve ConnectionString from appsettings.json 
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

// Use connectionString variable inside the AddDbContextPool that provided by EntityFrameworkCore
builder.Services.AddDbContextPool<AppDbContext>(option =>
    option.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICacheService, CacheService>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Redis
builder.Services.AddStackExchangeRedisCache(action =>
{
    action.Configuration = "localhost:6379";
});

// Caching
builder.Services.AddMemoryCache();


// #######################################################################

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