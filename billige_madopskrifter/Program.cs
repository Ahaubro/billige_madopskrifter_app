using billige_madopskrifter.Data;
using billige_madopskrifter.Helpers;
using billige_madopskrifter.Model;
using billige_madopskrifter.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql;
using static billige_madopskrifter.Helpers.JWTMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Connection string
var sv = new MySqlServerVersion(new Version(8, 0, 29));
var cs = "server=localhost;user=root;password=0000;database=billigmad";

builder.Services.AddDbContext<MyDBContext>(options => options.UseMySql(cs, sv));


//INJECT SERVICE KLASSER
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPasswordHelper, PasswordHash>();
builder.Services.AddTransient<IRecipeService, RecipeService>();
builder.Services.AddTransient<IIngredientService, IngredientService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
builder.Services.AddTransient<ILikedRecipesService, LikedRecipesService>();
builder.Services.AddTransient<IAllergieService, AllergieService>();
builder.Services.AddTransient<MyDBContext>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("JwtSettings", options))
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("CookieSettings", options));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

app.UseCors(configuration =>
                configuration.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
