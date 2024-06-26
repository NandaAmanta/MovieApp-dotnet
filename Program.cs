using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MovieApp.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MovieApp.Services;
using MovieApp.Utils;
using MovieApp.Middlewares;
using MovieApp.Configs;
using Coravel;
using MovieApp.Invocables;
using MovieApp.Queues;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:AccessTokenKey").Get<string>();

builder.Services.AddAuthorization();
builder.Services.AddScheduler();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = false,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

builder.Services.AddDbContext<MovieAppDataContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<TmdbConfiguration>(builder.Configuration.GetSection("Tmdb"));
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
});
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<StudioService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<MovieScheduleService>();
builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddSingleton<NotificationQueue>();
builder.Services.AddScoped<TokenGenerator>();
builder.Services.AddHostedService<NotificationSenderBgService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseMiddleware<AdminAuthorizeMiddleware>();

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<FetchMovieInvocable>()
        .DailyAtHour(0);
});
app.MapControllers();
app.Run();

