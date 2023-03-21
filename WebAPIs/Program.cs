using ECommerce.Application.UnitOfWork;
using ECommerce.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager cfm = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(cfm.GetConnectionString("ConnectionDB")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
