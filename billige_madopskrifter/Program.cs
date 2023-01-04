using billige_madopskrifter.Data;
using billige_madopskrifter.Helpers;
using billige_madopskrifter.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Connection string & adding MyDBContext
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


// Lavet med hjælp fra projektet https://github.com/Ahaubro/Wemuda-book-app 
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
