using ECommerce.Application.Helpers;
using ECommerce.Application.UnitOfWork;
using ECommerce.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager cfm = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// call DB
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(cfm.GetConnectionString("ConnectionDB")));
// call interface and class Unit of work 
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// call AutoMapper profile 
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
