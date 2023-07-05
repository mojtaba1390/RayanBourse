using Microsoft.EntityFrameworkCore;
using RayanBourse.Domain.Context;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("RayanBourseConnection");
builder.Services.AddDbContext<RayanBourseContext>(x => x.UseSqlServer(connectionString));





// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RayanBourseContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}



app.Run();

