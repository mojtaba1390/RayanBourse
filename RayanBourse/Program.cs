using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RayanBourse.Application;
using RayanBourse.Application.Interfaces;
using RayanBourse.Application.Services;
using RayanBourse.Domain.Context;
using RayanBourse.Domain.Entities;
using RayanBourse.Infrastructure;
using RayanBourse.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();

builder.Services.AddControllers();

//connection string and db
var connectionString = builder.Configuration.GetConnectionString("RayanBourseConnection");
builder.Services.AddDbContext<RayanBourseContext>(x => x.UseSqlServer(connectionString));

// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<RayanBourseContext>()
    .AddDefaultTokenProviders();

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("JWT:ValidAudience"),
        ValidIssuer = builder.Configuration.GetValue<string>("JWT:ValidIssuer"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Secret")))
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IProductService, ProductService>();



builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

//be sure that tables and database is created
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<RayanBourseContext>();
//    dbContext.Database.EnsureDeleted();
//    dbContext.Database.EnsureCreated();
//}


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint(
"v1/swagger.json", "V1"
    ));

app.Run();

