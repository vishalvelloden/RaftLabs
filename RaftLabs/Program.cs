using Microsoft.Extensions.Options;
using Proxy.Services.Clients;
using Proxy.Services.Objects.Configuration;
using RaftLabs.API.Middlewares;
using RaftLabs.Application.Handlers.Users.Queries.GetAllUser;
using RaftLabs.Application.Proxy;
using Shared.Common.AppSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetAllUserQuery).Assembly)
);

builder.Services.Configure<ExternalUserSettings>(
    builder.Configuration.GetSection("ExternalUserSettings"));

builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, CacheService>();


builder.Services.AddHttpClient<IExternalUserClient, ExternalUserClient>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ExternalUserSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseURL);
    client.DefaultRequestHeaders.Add("x-api-key", settings.ApiKey);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
