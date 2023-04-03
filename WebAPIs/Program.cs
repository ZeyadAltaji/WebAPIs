using ECommerce.Application.Helpers;
using ECommerce.Application.UnitOfWork;
using ECommerce.DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var secretKey = builder.Configuration.GetSection("AppSettings:key").Value;
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
ConfigurationManager cfm = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// call DB
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(cfm.GetConnectionString("ConnectionDB")));
// call interface and class Unit of work 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//call AutoMapper profile 
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

//Enable Cors
builder.Services.AddCors(x =>
{
    x.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = key
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
