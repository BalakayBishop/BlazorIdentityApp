using BlazorIdentityApp.Client;
using DataAccess.DbRepository;
using DataAccess.Entities;
using DataAccess.IDbRepository;
using Domain.IRepository;
using Domain.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped(s => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

string conn = builder.Configuration.GetConnectionString("dbConnectionString");

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

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7071")
});

await builder.Build().RunAsync();
