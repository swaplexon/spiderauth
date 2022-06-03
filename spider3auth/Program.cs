using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using spider3auth.Authorization;
using spider3auth.Helpers;
using spider3auth.Repository;
using spider3auth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
//builder.WebHost.UseIISIntegration();

// Configure JSON options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.IncludeFields = true;
});

// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// MongoDb Configuration
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoSettings>(serviceProvider =>
serviceProvider.GetRequiredService<IOptions<MongoSettings>>().Value
);
//builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

// configure DI for application services
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
