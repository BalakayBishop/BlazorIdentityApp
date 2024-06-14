using DataAccess.IDbRepository;
using DataAccess.DbRepository;
using DataAccess.Entities;
using Domain.IRepository;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string conn = builder.Configuration.GetConnectionString("LocalConnection");

builder.Services.AddDbContext<dbContext>(options => options.UseSqlServer(conn), ServiceLifetime.Transient);

builder.Services.AddTransient<IDbUsersRepository, DbUsersRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();

builder.Services.AddTransient<IDbPushNotificationsRepository, DbPushNotificationsRepository>();
builder.Services.AddTransient<IPushNotificationsRepository, PushNotificationsRepository>();

builder.Services.AddTransient<IDbUserNotificationsRepository, DbUserNotificationsRepository>();
builder.Services.AddTransient<IUserNotificationsRepository, UserNotificationsRepository>();

builder.Services.AddTransient<IDbRecordsRepository, DbRecordsRespository>();
builder.Services.AddTransient<IRecordsRepository, RecordsRepository>();

builder.Services.AddTransient<IDbUserRecordsRepository, DbUserRecordsRepository>();
builder.Services.AddTransient<IUserRecordsRepository, UserRecordsRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
            builder =>
            {
                builder.WithOrigins("https://localhost:7162")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
});

builder.Services.AddHttpClient();

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

app.UseCors("AllowSpecificOrigins");

app.Run();