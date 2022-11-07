using billige_madopskrifter.Data;
using billige_madopskrifter.Helpers;
using billige_madopskrifter.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sv = new MySqlServerVersion(new Version(8, 0, 29));
var cs = "server=localhost;user=root;password=0000;database=billigmad";
builder.Services.AddDbContext<DBContext>(options => options.UseMySql(cs, sv));


//INJECT SERVICE KLASSER
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPasswordHelper, PasswordHash>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("JwtSettings", options))
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("CookieSettings", options));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

app.UseCors(configuration =>
                configuration.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
